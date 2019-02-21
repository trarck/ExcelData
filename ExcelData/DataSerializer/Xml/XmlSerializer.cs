using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using System.Threading.Tasks;

namespace ExcelData.DataSerializer
{
    public class XmlSerializer : ISerializer
    {
        SerializerSettings m_Settings;

        public XmlSerializer()
        {
            m_Settings = new SerializerSettings();
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
