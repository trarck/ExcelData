using System;
using System.IO;
using System.Collections.Generic;
using TK.ExcelData;
using TK.Options;
namespace Generate
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
            List<string> codeFomates = new List<string>();
            List<string> dataFomates = new List<string>();

            optionSet = new OptionSet() {
                { "excelDir=", "Excel folder path", s => excelDir = s },
                { "codeOutDir=", "The code out folder", s => codeOutPath = s },
                { "dataOutPath=", "The data out folder", s => dataOutPath=s },
                { "codeNamespace=", "The code namspace", s => codeNamespace=s },
                { "codeFomate=", "Code language[CSharp,Cpp,Lua,Javascript]", s => codeFomates.Add (s) },
                { "dataFomate=", "Aata type [Json,Xml,Binary,LuaTable,UnityScriptable]", s => dataFomates.Add (s) },
                { "exportInfo=", "The last export info.", s => exportInfo=s }
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

            if(!string.IsNullOrEmpty(exportInfo) && !Path.IsPathRooted(exportInfo))
            {
                exportInfo = Path.Combine(Directory.GetCurrentDirectory(), exportInfo);
            }

            ExportSetting setting = new ExportSetting();
            ExcelExport export = new ExcelExport(setting);
            export.excelFolderPath = excelDir;
            export.codeOutFolderPath = codeOutPath;
            export.dataOutFolderPath = dataOutPath;
            export.codeNamespace = codeNamespace;
            ExcelExport.CodeFomate codeFomate = ExcelExport.CodeFomate.None;
            foreach (string fomate in codeFomates)
            {
                codeFomate |= (ExcelExport.CodeFomate)Enum.Parse(typeof(ExcelExport.CodeFomate), fomate);
            }
            export.codeFomate = codeFomate;

            ExcelExport.DataFomate dataFomate = ExcelExport.DataFomate.None;
            foreach (string fomate in dataFomates)
            {
                dataFomate |= (ExcelExport.DataFomate)Enum.Parse(typeof(ExcelExport.DataFomate), fomate);
            }
            export.dataFomate = dataFomate;

            export.exportInfoFile = exportInfo;
            export.Start();
        }
    }
}
