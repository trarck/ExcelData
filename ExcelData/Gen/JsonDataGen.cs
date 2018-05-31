using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TK.ExcelData
{
    public class JsonDataGen:DataGen
    {
        public override void Init()
        {
            base.Init();
        }

        public override void Generate(ISheet sheet,Schema schema, string outPath)
        {
            object list = DataReader.ReadList(sheet, schema);

            string tableName = sheet.SheetName;
            string filename = Path.Combine(outPath, (string.IsNullOrEmpty(tableName) ? schema.name : tableName) + ".json");
            SaveToJsonFile(filename, list);
        }

        void SaveToJsonFile(string jsonfile, object data)
        {
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.DateFormatString = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";

            string jsonString = JsonConvert.SerializeObject(data);
            File.WriteAllText(jsonfile, jsonString);
        }
    }
}