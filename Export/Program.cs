using System;
using System.IO;
using System.Collections.Generic;
using TK.ExcelData;
using TK.Options;

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

            List<string> codeFomates = new List<string>();
            List<string> dataFomates = new List<string>();

            optionSet = new OptionSet() {
                { "excelDir=", "Excel folder path", s => excelDir = s },
                { "codeOutDir=", "The code out folder", s => codeOutPath = s },
                { "dataOutPath=", "The data out folder", s => dataOutPath=s },
                { "codeNamespace=", "The code namspace", s => codeNamespace=s },
                { "codeFomate=", "Code language[CSharp,Cpp,Lua,Javascript]", s => codeFomates.Add (s) },
                { "dataFomate=", "Aata type [Json,Xml,Binary,LuaTable,UnityScriptable]", s => dataFomates.Add (s) },
                { "exportInfo=", "The last export info.", s => exportInfo=s },
                { "side=", "The last export info.", s => side=(Side)Enum.Parse(typeof(Side),s) },
                { "headModel=", "The last export info.", s => headModel=s }
            };

            optionSet.Parse(args);

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
                    default:
                        //use default side
                        break;
                }               
            }

            ExcelExport export = new ExcelExport(setting);
            export.excelFolderPath = excelDir;
            export.codeOutFolderPath = codeOutPath;
            export.dataOutFolderPath = dataOutPath;
            export.codeNamespace = codeNamespace;
            export.side = side;

            CodeFomate codeFomate = CodeFomate.None;
            foreach (string fomate in codeFomates)
            {
                codeFomate |= (CodeFomate)Enum.Parse(typeof(CodeFomate), fomate);
            }
            export.codeFomate = codeFomate;

            DataFomate dataFomate = DataFomate.None;
            foreach (string fomate in dataFomates)
            {
                dataFomate |= (DataFomate)Enum.Parse(typeof(DataFomate), fomate);
            }
            export.dataFomate = dataFomate;

            export.exportInfoFile = exportInfo;
            export.Start();
        }
    }
}
