using System.Collections.Generic;
using System.IO;

namespace TK.ExcelData
{
    public class CSharpGen:CodeGen
    {
        /// <summary>
        /// 
        /// </summary>
        static string TemplateFile = "ExcelData/Gen/Templates/CSharpClass.ts";

        public override void Init(string templateFilePath)
        {
            base.Init(templateFilePath);

            m_TemplateContent= System.IO.File.ReadAllText(templateFilePath);
        }

        public override void Generate(Schema schema,string outPath)
        {
            string content = CreateClass(schema);

            //check path is exists
            if (!Directory.Exists(outPath))
            {
                Directory.CreateDirectory(outPath);
            }
            string codeFile = Path.Combine(outPath, schema.className + m_GenExt);
            File.WriteAllText(codeFile, content);
        }

        string GetFieldTypeDefine(Field field)
        {
            return field.type.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        protected override string CreateProperty(Field field)
        {
            string pad = Pad(m_PropertyPad);

            string comment = "";

            if (!string.IsNullOrEmpty(field.description))
            {
                comment = field.description;
            }

            if (!string.IsNullOrEmpty(field.comment))
            {
                if (string.IsNullOrEmpty(comment))
                {
                    comment = field.comment;
                }
                else
                {
                    comment += m_CRLF + field.comment;
                }
            }

            if (!string.IsNullOrEmpty(comment))
            {
                comment = pad + "/// <summary>" + m_CRLF
                        + ParseLines(comment, pad)
                        + pad + "/// </summary>"+m_CRLF;
            }

            return comment
                + pad+"public " + GetFieldTypeDefine(field) + " " + field.name+" { get;set;}"+ m_CRLF;
        }

        protected string ParseLines(string str,string pad)
        {
            string[] lines = str.Replace("\r\n", "\n").Split('\n');
            string ret = "";
            for(int i = 0; i < lines.Length; ++i)
            {
                ret += pad + "/// " + lines[i] + m_CRLF;
            }
            return ret;
        }
    }
}