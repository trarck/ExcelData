using System;
using System.Collections;
using System.Xml;

namespace ExcelData.DataSerializer
{
    public class XmlSerialization : ISerialization
    {
        private XmlWriter m_XmlWrite;
        public string defaultSchema;

        public XmlSerialization(XmlWriter xmlWriter)
        {
            m_XmlWrite = xmlWriter;
        }

        public void Serialize(object value)
        {
            m_XmlWrite.WriteStartDocument();
            Visit(value);
            m_XmlWrite.WriteEndDocument();
            m_XmlWrite.Flush();
        }

        void Visit(object value)
        {
            if(value is IList)
            {
                Visit(value as IList);
            }
            else if(value is IDictionary)
            {
                Visit(value as IDictionary);
            }
            else
            {
                Type type = value.GetType();
                switch (type)
                {
                    case Int32:
                }
            }
        }

        void Visit(IList list)
        {

        }

        void Visit(IDictionary dict)
        {

        }
    }
}
