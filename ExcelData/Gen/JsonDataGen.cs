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

        public override void Generate(ISheet sheet,Schema schema, string outputFile)
        {
            object list = ReadHelper.ReadList(sheet, schema);

            string folder = Path.GetDirectoryName(outputFile);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            SaveToJsonFile(outputFile, list);
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