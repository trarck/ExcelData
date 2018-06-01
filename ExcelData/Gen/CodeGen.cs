using System.Collections.Generic;
using System.IO;

namespace TK.ExcelData
{
    public abstract class CodeGen
    {
        protected string m_CRLF = "\r\n";  
        protected int m_PropertyPad = 4;
        protected string m_Ns;
        protected string m_TemplateContent;

        protected string m_TemplateFilePath;

        protected string m_GenExt =".cs";

        public virtual void Init(string templateFilePath)
        {
            m_TemplateFilePath = templateFilePath;
        }

        public virtual void Generate(Schema schema,string outPath)
        {

        }

        public virtual string CreateClass(Schema schema)
        {
            if (string.IsNullOrEmpty(m_TemplateContent))
            {
                return null;
            }

            string content = m_TemplateContent;
            content = content.Replace("{CLASS}", schema.name);
            content = content.Replace("{PROPERTIES}", CreateProperties(schema));
            if (string.IsNullOrEmpty(m_Ns))
            {
                content = content.Replace("{NAMESPACE_START}", "");
                content = content.Replace("{NAMESPACE_END}", "");
            }
            else
            {
                content = content.Replace("{NAMESPACE_START}", "namespace " + m_Ns + "\n{");
                content = content.Replace("{NAMESPACE_END}", "}");
            }

            return content;
        }

        protected virtual string CreateProperties(Schema shema)
        {
            string properties = "";
            foreach (Field field in shema.fields)
            {
                properties += CreateProperty(field);
            }
            return properties;
        }

        protected virtual string CreateProperty(Field field)
        {
            return null;
        }

        public static string Pad(int num)
        {
            string p = "";
            for(int i = 0; i < num; ++i)
            {
                p += " ";
            }

            return p;
        }

        public string crlf
        {
            get
            {
                return m_CRLF;
            }
            set
            {
                m_CRLF = value;
            }
        }

        public int propertyPad
        {
            get
            {
                return m_PropertyPad;
            }
            set
            {
                m_PropertyPad = value;
            }
        }

        public string ns
        {
            set
            {
                m_Ns = value;
            }

            get
            {
                return m_Ns;
            }
        }

        public string templateContent
        {
            get
            {
                return m_TemplateContent;
            }
            set
            {
                m_TemplateContent = value;
            }
        }

        public string templateFilePath
        {
            get
            {
                return m_TemplateFilePath;
            }
            set
            {
                m_TemplateFilePath = value;
            }
        }
    }
}