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
            object list = ReadHelper.ReadList(sheet, schema);

            string tableName = schema.name;
            if (!Directory.Exists(outPath))
            {
                Directory.CreateDirectory(outPath);
            }
            string filename = Path.Combine(outPath, tableName + m_ExportExt);
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