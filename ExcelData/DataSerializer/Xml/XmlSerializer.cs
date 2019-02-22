using System;
using System.IO;
using System.Xml;

namespace ExcelData.DataSerializer
{
    public class XmlSerializer : ISerializer
    {
        SerializerSettings m_Settings;

        public XmlSerializer()
        {
        }

        public XmlSerializer(SerializerSettings settings)
        {
            m_Settings = settings;
        }

        public void Serialize(object value, Stream stream)
        {
            using (XmlWriter xmlWriter = XmlWriter.Create(stream))
            {

            }
        }

        public object Deserialize(Stream steam)
        {
            throw new NotImplementedException();
        }
    }
}
