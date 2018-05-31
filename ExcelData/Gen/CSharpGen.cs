using System.Collections.Generic;
using System.IO;

namespace TK.ExcelData
{
    public class CSharpGen:CodeGen
    {
        static string TemplateFile = "ExcelData/Gen/CodeDataTemplate.ts";

        public override void Init(string templateFilePath)
        {
            base.Init(templateFilePath);

            m_TemplateContent= System.IO.File.ReadAllText(templateFilePath);
        }

        public override void Generate(Schema schema,string outputPath)
        {
            string content = CreateClass(schema);

            //check path is exists
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            string codeFile = Path.Combine(outputPath , schema.name + ".cs");
            File.WriteAllText(codeFile, content);
        }

        string GetFieldTypeDefine(Field field)
        {
            string typeDefine = "object";

            switch (field.type)
            {
                case ExcelDataType.String:
                    typeDefine = "string";
                    break;
                case ExcelDataType.Int:
                    typeDefine = "int";
                    break;
                case ExcelDataType.Float:
                    typeDefine = "float";
                    break;
                case ExcelDataType.Array:
                    typeDefine = (field.extType==""?"string":field.extType)+"[]";
                    break;
                case ExcelDataType.List:
                    typeDefine = "List<"+ (field.extType == "" ? "object" : field.extType) + ">";
                    break;
                case ExcelDataType.Dictionary:                    
                    typeDefine = "Dictionary<"+ (field.extType == "" ? "string,object" : field.extType) + ">";
                    break;
                case ExcelDataType.Long:
                    typeDefine = "long";
                    break;
                case ExcelDataType.Double:
                    typeDefine = "double";
                    break;
                case ExcelDataType.Custom:
                    typeDefine = field.extType;
                    break;
            }

            return typeDefine;
        }

        protected override string CreateProperty(Field field)
        {
            string pad = Pad(m_PropertyPad);

            string comment = "";
            if (field.comment != "")
            {
                comment = pad + "/*" + field.comment + "*/"+ m_CRLF;
            }
            return comment
                + pad+"public " + GetFieldTypeDefine(field) + " " + field.name+";"+ m_CRLF;
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
    }
}