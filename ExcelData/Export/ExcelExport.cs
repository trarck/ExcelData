using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace TK.ExcelData
{
    public class ExportInfo
    {
        public DateTime lastExport;
        public string md5;
    }

    public class ExcelExport
    {
        public enum ExportResult
        {
            Success,
            NotExcelFile,
            Error
        }

        public enum CodeFomate
        {
            None=0,
            CSharp=1,
            Cpp=2,
            Lua=4,
            Javascript=8
        }

        public enum DataFomate
        {
            None=0,
            Json=1,
            Xml=2,
            Binary=4,
            LuaTable=8,
            UnityScriptable=16
        }

        public string excelFolderPath { get; set; }
        public string codeOutFolderPath{get;set;}
        public string dataOutFolderPath { get; set; }
        public string codeNamespace { get; set; }
        public CodeFomate codeFomate { get; set; }
        public DataFomate dataFomate { get; set; }
        public string exportInfoFile { get; set; }

        public ExportSetting setting { get; set; }

        private Dictionary<string, ExportInfo> m_ExportInfos;

        public ExcelExport(ExportSetting setting)
        {
            this.setting = setting;
        }

        public void Start()
        {
            if (!Directory.Exists(excelFolderPath))
            {
                return;
            }

            string[] excelFiles = Directory.GetFiles(excelFolderPath, "*.xls", SearchOption.AllDirectories);
            if(excelFiles==null || excelFiles.Length == 0)
            {
                //没有要导出的表。
                return;
            }

            //检查是否有导出记录。不再导出没有改变的表
            if (!string.IsNullOrEmpty(exportInfoFile))
            {
                m_ExportInfos = LoadExportInfo(exportInfoFile);
                if (m_ExportInfos == null)
                {
                    m_ExportInfos = new Dictionary<string, ExportInfo>();
                }
            }
            else
            {
                m_ExportInfos = null;
            }

            ExportInfo info;
            string currentMd5;
            foreach (var excelFile in excelFiles)
            {
                if (m_ExportInfos != null)
                {
                    //比对上次导出信息
                    string relativePath = Relative(excelFolderPath,excelFile);
                    currentMd5 = ComputeMD5(excelFile);
                    if (m_ExportInfos.TryGetValue(relativePath, out info))
                    {
                        //不一致重新导出
                        if (currentMd5 != info.md5)
                        {
                            if (Export(excelFile) == ExportResult.Success)
                            {
                                //导出成功，保存信息
                                info.md5 = currentMd5;
                                info.lastExport = DateTime.Now;
                            }
                        }
                    }
                    else
                    {
                        //不存在则导出
                        if (Export(excelFile) == ExportResult.Success)
                        {
                            //导出成功，保存信息
                            info = new ExportInfo();
                            info.md5 = currentMd5;
                            info.lastExport = DateTime.Now;
                            m_ExportInfos[relativePath] = info;
                        }
                    }
                }
                else
                {
                    //不需要保存
                    Export(excelFile);
                }                
            }
            //保存导出信息
            if (!string.IsNullOrEmpty(exportInfoFile))
            {
                SaveExportInfo(exportInfoFile, m_ExportInfos);
            }
        }

        public ExportResult Export(string excelFilePath)
        {
            IWorkbook workbook = ExcelHelper.Load(excelFilePath);
            if (workbook != null)
            {
                for (int i = 0; i < workbook.NumberOfSheets; ++i)
                {
                    ISheet sheet = workbook.GetSheetAt(i);
                    if (setting.ExportCheck(sheet))
                    {
                        ExportSheet(sheet);
                    }
                }
            }
            else
            {
                return ExportResult.NotExcelFile;
            }

            return ExportResult.Success;
        }

        private ExportResult ExportSheet(ISheet sheet)
        {
            Schema schema = SchemaReader.ReadSchema(sheet,setting.headModel);
            schema.name = setting.FomateSheetName(sheet);

            if (codeFomate != CodeFomate.None)
            {
                GenerateCode(schema);
            }

            if (dataFomate != DataFomate.None)
            {
                ExportData(sheet, schema);
            }

            return ExportResult.Success;
        }

        /// <summary>
        /// 生成code
        /// </summary>
        /// <param name="schema"></param>
        private void GenerateCode(Schema schema)
        {
            ClassCodeGen codeGen = new ClassCodeGen();
            codeGen.Init();
            codeGen.schema = schema;
            codeGen.ns = codeNamespace;
            codeGen.SynicSession();

            if ((codeFomate & CodeFomate.CSharp)!=0)
            {
                codeGen.inputFile = setting.GetCodeTemplate(CodeFomate.CSharp);
                string outFile = null;
                if (setting.codeUseSubPath)
                {
                    outFile = Path.Combine(codeOutFolderPath,CodeFomate.CSharp.ToString(), schema.name + ".cs");
                }
                else
                {
                    outFile = Path.Combine(codeOutFolderPath, schema.name + ".cs");
                }
                

                codeGen.Generate(outFile);
            }

            if ((codeFomate & CodeFomate.Cpp) != 0)
            {
                codeGen.inputFile = setting.GetCodeTemplate(CodeFomate.Cpp);
                string outFile = null;
                if (setting.codeUseSubPath)
                {
                    outFile = Path.Combine(codeOutFolderPath, CodeFomate.Cpp.ToString(), schema.name + ".cpp");
                }
                else
                {
                    outFile = Path.Combine(codeOutFolderPath, schema.name + ".cpp");
                }
                codeGen.Generate(outFile);
            }

            if ((codeFomate & CodeFomate.Lua) != 0)
            {
                codeGen.inputFile = setting.GetCodeTemplate(CodeFomate.Lua);
                string outFile = null;
                if (setting.codeUseSubPath)
                {
                    outFile = Path.Combine(codeOutFolderPath, CodeFomate.Lua.ToString(), schema.name + ".lua");
                }
                else
                {
                    outFile = Path.Combine(codeOutFolderPath, schema.name + ".lua");
                }
                codeGen.Generate(outFile);
            }

            if ((codeFomate & CodeFomate.Javascript) != 0)
            {
                codeGen.inputFile = setting.GetCodeTemplate(CodeFomate.Javascript);
                string outFile = null;
                if (setting.codeUseSubPath)
                {
                    outFile = Path.Combine(codeOutFolderPath, CodeFomate.Javascript.ToString(), schema.name + ".js");
                }
                else
                {
                    outFile = Path.Combine(codeOutFolderPath, schema.name + ".js");
                }
                codeGen.Generate(outFile);
            }
        }

        public void ExportData(ISheet sheet,Schema schema)
        {
            if ((dataFomate & DataFomate.Json) != 0)
            {
                ExportJsonData(sheet, schema);
            }

            if ((dataFomate & DataFomate.Xml) != 0)
            {
                ExportXmlData(sheet, schema);
            }

            if ((dataFomate & DataFomate.Binary) != 0)
            {
                ExportBinaryData(sheet, schema);
            }

            if ((dataFomate & DataFomate.LuaTable) != 0)
            {
                ExportLuaTable(sheet, schema);
            }

            if ((dataFomate & DataFomate.UnityScriptable) != 0)
            {
                ExportUnityScriptable(sheet, schema);
            }
        }

        private void ExportJsonData(ISheet sheet,Schema schema)
        {
            DataGen gen = new JsonDataGen();
            string savePath = dataOutFolderPath;
            if (setting.dataUseSubPath)
            {
                savePath = Path.Combine(savePath, DataFomate.Json.ToString());
            }
            string outputFile = Path.Combine(savePath, schema.name + gen.exportExt);
            gen.Generate(sheet, schema,setting.headModel, outputFile);
        }

        private void ExportXmlData(ISheet sheet, Schema schema)
        {
            string savePath = dataOutFolderPath;
            if (setting.dataUseSubPath)
            {
                savePath = Path.Combine(savePath, DataFomate.Xml.ToString());
            }
        }

        private void ExportBinaryData(ISheet sheet, Schema schema)
        {
            string savePath = dataOutFolderPath;
            if (setting.dataUseSubPath)
            {
                savePath = Path.Combine(savePath, DataFomate.Binary.ToString());
            }
        }

        private void ExportLuaTable(ISheet sheet, Schema schema)
        {
            string savePath = dataOutFolderPath;
            if (setting.dataUseSubPath)
            {
                savePath = Path.Combine(savePath, DataFomate.LuaTable.ToString());
            }
        }

        private void ExportUnityScriptable(ISheet sheet, Schema schema)
        {
            string savePath = dataOutFolderPath;
            if (setting.dataUseSubPath)
            {
                savePath = Path.Combine(savePath, DataFomate.UnityScriptable.ToString());
            }
        }

        private static Dictionary<string,ExportInfo> LoadExportInfo(string exportInfoFile)
        {
            if (!File.Exists(exportInfoFile))
            {
                return null;
            }

            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.DateFormatString = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";

            using (StreamReader file = File.OpenText(exportInfoFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                Dictionary<string, ExportInfo> infos = serializer.Deserialize(file, typeof(Dictionary<string, ExportInfo>)) as Dictionary<string, ExportInfo>;
                return infos;
            }
        }

        private static void SaveExportInfo(string exportInfoFile,Dictionary<string,ExportInfo> infos)
        {
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.DateFormatString = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";

            using (StreamWriter file = File.CreateText(exportInfoFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, infos);
            }
        }

        private static string Relative(string fromPath, string toPath)
        {
            fromPath = fromPath.Replace("\\", "/");
            toPath = toPath.Replace("\\", "/");

            if (fromPath[fromPath.Length - 1] == '/')
            {
                fromPath = fromPath.Substring(0, fromPath.Length - 1);
            }

            if (toPath[toPath.Length - 1] == '/')
            {
                toPath = toPath.Substring(0, toPath.Length - 1);
            }

            string[] froms = fromPath.Split('/');
            string[] tos = toPath.Split('/');

            int i = 0;
            //look for same part
            for (int l=froms.Length>tos.Length?tos.Length: froms.Length; i < l; ++i)
            {
                if (froms[i] != tos[i])
                {
                    break;
                }
            }

            if (i == 0)
            {
                //just windows. eg.fromPath=c:\a\b\c,toPath=d:\e\f\g
                //if linux the first is empty always same. eg. fromPath=/a/b/c,toPath=/d/e/f
                return toPath;
            }
            else
            {
                System.Text.StringBuilder result = new System.Text.StringBuilder();
                System.Text.StringBuilder toSB = new System.Text.StringBuilder();

                for (int j = i; j < froms.Length; ++j)
                {
                    result.Append("../");
                }

                for (int j = i; j < tos.Length; ++j)
                {
                    result.Append(tos[j]);
                    if (j < tos.Length - 1)
                    {
                        result.Append("/");
                    }
                }
                return result.ToString();
            }
        }

        private static string ComputeMD5(string fileName)
        {
            string hashMD5 = string.Empty;
            //检查文件是否存在，如果文件存在则进行计算，否则返回空值
            if (System.IO.File.Exists(fileName))
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    //计算文件的MD5值
                    System.Security.Cryptography.MD5 calculator = System.Security.Cryptography.MD5.Create();
                    Byte[] buffer = calculator.ComputeHash(fs);
                    calculator.Clear();
                    //将字节数组转换成十六进制的字符串形式
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        stringBuilder.Append(buffer[i].ToString("x2"));
                    }
                    hashMD5 = stringBuilder.ToString();
                }//关闭文件流
            }//结束计算
            return hashMD5;
        }
    }
}
