using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NPOI.SS.UserModel;
using TK.ExcelData;

namespace Generate
{
    class Program
    {
        static void Main(string[] args)
        {
            string excelFile=null;
            string classOutPath=null;
            string dataOutPath = null;

            if (args.Length == 0)
            {
                Console.WriteLine("cmd excelFile outPath");
            }
            else if (args.Length == 1)
            {
                excelFile = args[0];
                classOutPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Class");
                dataOutPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Data");
            }
            else if (args.Length == 2)
            {
                excelFile = args[0];
                classOutPath = Path.Combine(args[1], "Class");
                dataOutPath = Path.Combine(args[1], "Data");
            }
            else
            {
                excelFile = args[0];
                classOutPath = args[1];
                dataOutPath = args[2];
            }

            string workPath = System.IO.Directory.GetCurrentDirectory();
            GenWorkbook(Path.Combine(workPath,excelFile), Path.Combine(workPath,classOutPath),Path.Combine(workPath, dataOutPath));
        }

        static void GenWorkbook(string excelFile,string classOutPath,string dataOutPath)
        {
            IWorkbook workbook = ExcelHelper.Load(excelFile);
            if (workbook != null)
            {
                for (int i = 0; i < workbook.NumberOfSheets; ++i)
                {
                    ISheet sheet = workbook.GetSheetAt(i);
                    if (ExcelHelper.NeedExport(sheet))
                    {
                        GenSheet(sheet, classOutPath, dataOutPath,ExcelHelper.GetExportName(sheet));
                    }
                }
            }
            else
            {
                Console.WriteLine("Open " + excelFile + " fail");
            }
        }

        static void GenSheet(ISheet sheet, string classOutPath,string dataOutPath, string schemaName,string genNamespace="")
        {
            Schema schema = SchemaReader.ReadSchema(sheet);
            schema.name = schemaName;

            GenClass(schema, classOutPath, genNamespace);

            GenData(sheet, schema, dataOutPath);
        }

        static void GenClass(Schema schema, string savePath, string genNamespace = "")
        {
            CodeGen gen = new CSharpGen();
            gen.Init(Path.Combine(Directory.GetCurrentDirectory(),"ExcelData/Gen/CodeDataTemplate.ts"));

            gen.ns = genNamespace;
            gen.Generate(schema, savePath);
        }


        static void GenData(ISheet sheet,Schema schema, string savePath)
        {
            DataGen gen = new JsonDataGen();

            gen.Generate(sheet,schema, savePath);
        }
    }
}
