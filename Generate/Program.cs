using System;
using System.IO;
using NPOI.SS.UserModel;
using TK.ExcelData;

namespace Generate
{
    class Program
    {
       
        static void Main(string[] args)
        {
            TypeInfo ti1 = TypeInfo.Parse("Int");
            TypeInfo ti2 = TypeInfo.Parse("int");
            Console.WriteLine(ti1 == ti2);

            TypeInfo tlist = TypeInfo.Parse("list<int>");
            Console.WriteLine(tlist.genericType.name);
            Console.WriteLine(tlist.genericArguments[0].name);

            TypeInfo tdict= TypeInfo.Parse("Dictionary<int,string>");
            Console.WriteLine(tdict.genericType.name);
            Console.WriteLine(tdict.genericArguments[0].name);
            Console.WriteLine(tdict.genericArguments[1].name);
            
            TypeInfo ttest= TypeInfo.Parse("Test<int,string>");
            Console.WriteLine(ttest.genericType.name);
            Console.WriteLine(ttest.genericArguments[0].name);
            Console.WriteLine(ttest.genericArguments[1].name);

            TypeInfo tm1 = TypeInfo.Parse("List<Dictionary<int,string>>");
            TypeInfo tm2 = TypeInfo.Parse("List<Dictionary<int,List<Dictionary<int,string>>>>");
            TypeInfo tm3 = TypeInfo.Parse("Dictionary<Dictionary<int,List<Dictionary<int,string>>>,Dictionary<int,List<Dictionary<int,string>>>>");
            TypeInfo tm4 = TypeInfo.Parse("My<int,Dictionary<int,string>,String,List<Dictionary<int,string>>>");

            Console.WriteLine(tm3);

            string excelFile = null;
            string classOutPath = null;
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
           // GenWorkbook(Path.Combine(workPath, excelFile), Path.Combine(workPath, classOutPath), Path.Combine(workPath, dataOutPath));
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

            GenCSharpClass(schema, classOutPath, genNamespace);

            GenJsonData(sheet, schema, dataOutPath);
        }

        static void GenCSharpClass(Schema schema, string savePath, string genNamespace = "")
        {
            CodeGen gen = new CSharpGen();
            gen.Init(Path.Combine(Directory.GetCurrentDirectory(),"ExcelData/Gen/Templates/CSharpClass.ts"));

            gen.ns = genNamespace;
            gen.Generate(schema, savePath);
        }


        static void GenJsonData(ISheet sheet,Schema schema, string savePath)
        {
            DataGen gen = new JsonDataGen();

            gen.Generate(sheet,schema, savePath);
        }

        static void GenXmlData(ISheet sheet, Schema schema, string savePath)
        {
            DataGen gen = new XmlDataGen();

            gen.Generate(sheet, schema, savePath);
        }
    }
}
