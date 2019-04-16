using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Collections;

namespace TK.ExcelData
{
    public class ExcelImport
    {
        public ImportSetting setting { get; set; }
        public DataFormat dataFormat { get; set; }
        public Type dataType { get; set; }
        public Side side { get; set; }

        public ExcelImport(ImportSetting setting)
        {
            this.setting = setting;
        }

        public ImportResult Import(string dataFilePath,string excelFilePath,string sheetName=null)
        {
            IWorkbook workbook = ExcelHelper.Load(excelFilePath);
            if (workbook != null)
            {
                if (string.IsNullOrEmpty(sheetName))
                {
                    sheetName = Path.GetFileNameWithoutExtension(dataFilePath);
                }

                ISheet sheet=workbook.GetSheet(sheetName);
                if (sheet!=null)
                {
                    Import(sheet,dataFilePath);
                    ExcelHelper.Save(excelFilePath, workbook);
                }
                else
                {
                    return ImportResult.NotSheet;
                }
            }
            else
            {
                return ImportResult.NotExcelFile;
            }
            return ImportResult.Success;
        }

        private ExportResult Import(ISheet sheet,string dataFilePath)
        {
            Schema schema = SchemaReader.ReadSchema(sheet, setting.headModel);

            if (dataFormat != DataFormat.None)
            {
                ImportData(sheet, schema,dataFilePath);
            }

            return ExportResult.Success;
        }

        public void ImportData(ISheet sheet, Schema schema,string dataFilePath)
        {
            if ((dataFormat & DataFormat.Json) != 0)
            {
                ImportJsonData(sheet, schema, dataFilePath);
            }
        }

        private void ImportJsonData(ISheet sheet, Schema schema, string dataFilePath)
        {
            string content = File.ReadAllText(dataFilePath);
            object jsonObj = JsonConvert.DeserializeObject(content,dataType);
            DataWriter dataWriter = new DataWriter();
            dataWriter.side = side;

            if(jsonObj is IList)
            {
                dataWriter.WriteList(sheet, schema, jsonObj as IList,setting.headModel);
            }
            else if(jsonObj is IDictionary)
            {
                dataWriter.WriteDictionary(sheet, schema, jsonObj as IDictionary, setting.headModel);
            }
        }
    }
}
