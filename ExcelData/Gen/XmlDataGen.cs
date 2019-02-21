using NPOI.SS.UserModel;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace TK.ExcelData
{
    public class XmlDataGen:DataGen
    {
        public override void Init()
        {
            base.Init();
        }

        public override void Generate(ISheet sheet,Schema schema, string outPath)
        {
            object list = DataReader.ReadList(sheet, schema);

            string tableName = schema.name;
            if (!Directory.Exists(outPath))
            {
                Directory.CreateDirectory(outPath);
            }
            string filename = Path.Combine(outPath, tableName + m_ExportExt);
            SaveToXmlFile(filename, list);
        }

        void SaveToXmlFile(string xmlfile, object data)
        {
            string jsonString = JsonConvert.SerializeObject(data);           
            XmlDocument doc = JsonConvert.DeserializeXmlNode(jsonString);
            doc.Save(xmlfile);
        }
    }
}