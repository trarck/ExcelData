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
            string outPath=null;
            if (args.Length < 1)
            {
                Console.WriteLine("cmd excelFile outPath");
            }
            else if (args.Length < 2)
            {
                excelFile = args[0];
                outPath = System.IO.Directory.GetCurrentDirectory();
            }
            else
            {
                excelFile = args[0];
                outPath = args[1];
            }

            string workPath = System.IO.Directory.GetCurrentDirectory();
            GenWorkbook(Path.Combine(workPath,excelFile), Path.Combine(workPath,outPath));
        }

        static void GenWorkbook(string excelFile,string savePath)
        {
            IWorkbook workbook = ExcelHelper.Load(excelFile);

            for (int i = 0; i < workbook.NumberOfSheets; ++i)
            {
                ISheet sheet = workbook.GetSheetAt(i);
                if (ExcelHelper.IsTableSheet(sheet))
                {
                    GenSheet(sheet,savePath,sheet.SheetName);
                }
            }
        }

        static void GenSheet(ISheet sheet, string savePath, string schemaName,string genNamespace="")
        {
            Schema schema = SchemaReader.ReadSchema(sheet);
            schema.name = schemaName;

            GenClass(schema, savePath, genNamespace);

            GenData(sheet, schema, savePath);
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
