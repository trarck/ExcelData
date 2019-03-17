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

        public override void Generate(ISheet sheet,Schema schema, HeadModel headModel, string outputFile)
        {
            DataReader dataReader = new DataReader();
            dataReader.headModel = headModel;

            object list = dataReader.ReadList(sheet, schema, headModel);

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