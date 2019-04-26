using System;
using System.IO;
using System.Collections.Generic;
using TK.ExcelData;
using TK.Options;
using TK.Excel;

namespace Export
{
    class Program
    {
        static OptionSet optionSet;

        static void Main(string[] args)
        {
            string excelDir = null;
            string codeOutPath = null;
            string dataOutPath = null;
            string codeNamespace = "";
            string exportInfo = null;
            Side side = Side.All;
            string headModel = null;
            string templateFolder = null;

            List<string> codeFormats = new List<string>();
            List<string> dataFormats = new List<string>();
            
            optionSet = new OptionSet() {
                { "excelDir=", "Excel folder path", s => excelDir = s },
                { "codeOutDir=", "The code out folder", s => codeOutPath = s },
                { "dataOutPath=", "The data out folder", s => dataOutPath=s },
                { "codeNamespace=", "The code namspace", s => codeNamespace=s },
                { "codeFormat=", "Code language[CSharp,Cpp,Lua,Javascript]", s => codeFormats.Add (s) },
                { "dataFormat=", "Aata type [Json,Xml,Binary,LuaTable,UnityScriptable]", s => dataFormats.Add (s) },
                { "exportInfo=", "The last export info.", s => exportInfo=s },
                { "side=", "The last export info.", s => side=(Side)Enum.Parse(typeof(Side),s) },
                { "headModel=", "The last export info.", s => headModel=s },
                { "templatePath=", "The last export info.", s => templateFolder=s }
            };

            optionSet.Parse(args);

            if (string.IsNullOrEmpty(excelDir))
            {
                System.Console.WriteLine("Excel path is null");
                return;
            }

            if (string.IsNullOrEmpty(codeOutPath))
            {
                System.Console.WriteLine("Code out path is null");
                return;
            }

            if (string.IsNullOrEmpty(dataOutPath))
            {
                System.Console.WriteLine("Data out path is null");
                return;
            }


            if (!Path.IsPathRooted(excelDir))
            {
                excelDir = Path.Combine(Directory.GetCurrentDirectory(), excelDir);
            }

            if (!Path.IsPathRooted(codeOutPath))
            {
                codeOutPath = Path.Combine(Directory.GetCurrentDirectory(), codeOutPath);
            }

            if (!Path.IsPathRooted(dataOutPath))
            {
                dataOutPath = Path.Combine(Directory.GetCurrentDirectory(), dataOutPath);
            }

            if (!string.IsNullOrEmpty(exportInfo) && !Path.IsPathRooted(exportInfo))
            {
                exportInfo = Path.Combine(Directory.GetCurrentDirectory(), exportInfo);
            }

            ExportSetting setting = new ExportSetting();
            if (!string.IsNullOrEmpty(headModel))
            {
                switch (headModel)
                {
                    case "Normal":
                        setting.headModel = HeadModel.CreateNormalModel();
                        break;
                    case "Simple":
                        setting.headModel = HeadModel.CreateSimpleModel();
                        break;
                    default://All
                        //use default have side
                        break;
                }
            }

            if (!string.IsNullOrEmpty(templateFolder))
            {
                setting.templateFolder = templateFolder;
            }

            ExcelExport export = new ExcelExport(setting);
            export.excelFolderPath = excelDir;
            export.codeOutFolderPath = codeOutPath;
            export.dataOutFolderPath = dataOutPath;
            export.codeNamespace = codeNamespace;
            export.side = side;


            CodeFomat codeFormat = CodeFomat.None;
            foreach (string format in codeFormats)
            {
                codeFormat |= (CodeFomat)Enum.Parse(typeof(CodeFomat), format);
            }
            export.codeFormat = codeFormat;

            DataFormat dataFormat = DataFormat.None;
            foreach (string format in dataFormats)
            {
                dataFormat |= (DataFormat)Enum.Parse(typeof(DataFormat), format);
            }
            export.dataFormat = dataFormat;

            export.exportInfoFile = exportInfo;
            export.Start();
        }
    }
}
