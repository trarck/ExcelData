using System;
using System.IO;
using TK.Excel;
using TK.ExcelData;
using TK.Options;

namespace Import
{
    class Program
    {
        static OptionSet optionSet;

        static void Main(string[] args)
        {
            string excelFile = null;
            string dataFile = null;
            Side side = Side.All;
            string headModel = null;
            string dataFormatStr = null;
            string dataType = "List<Dictionary<string, object>>";

            optionSet = new OptionSet() {
                { "excelFile=", "Excel folder path", s => excelFile = s },
                { "dataFile=", "The code out folder", s => dataFile = s },
                { "headModel=", "The last export info.", s => headModel=s },
                { "side=", "The last export info.", s => side=(Side)Enum.Parse(typeof(Side),s) },
                {"dataFormat=","Data format",s=>dataFormatStr=s },
                {"dataType=","Data Type",s=>dataType=s },
            };

            optionSet.Parse(args);

            if (string.IsNullOrEmpty(excelFile))
            {
                Console.WriteLine("Excel file is null");
                return;
            }

            if (string.IsNullOrEmpty(dataFile))
            {
                Console.WriteLine("Code out path is null");
                return;
            }

            if (!Path.IsPathRooted(excelFile))
            {
                excelFile = Path.Combine(Directory.GetCurrentDirectory(), excelFile);
            }

            if (!Path.IsPathRooted(dataFile))
            {
                dataFile = Path.Combine(Directory.GetCurrentDirectory(), dataFile);
            }

            ImportSetting setting = new ImportSetting();
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
                    //use default have side
                    case "All":
                    default:
                        break;
                }
            }

            ExcelImport import = new ExcelImport(setting);
            import.side = side;


            DataFormat dataFormat = (DataFormat)Enum.Parse(typeof(DataFormat), dataFormatStr);
            import.dataFormat = dataFormat;
            import.dataType = TypeInfo.Parse(dataType).ToSystemType();

            import.Import(dataFile,excelFile);
        }
    }
}
