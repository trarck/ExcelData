using System.IO;
using TK.Excel;

namespace TK.ExcelData
{
    public class CodeGenTemplateCSharp:CodeGenTemplate
    {
        protected override void WriteHeader(TextWriter stream)
        {
            stream.WriteLine("//!!!This file is generated don't modify!!!");
            stream.WriteLine("using System;");
            stream.WriteLine("using System.Collections;");
            stream.WriteLine("using System.Collections.Generic;");
        }

        protected override void WriteFooter(TextWriter stream)
        {

        }

        protected override void WriteContent(TextWriter stream, Schema schema)
        {
            //write name space  start
            WriteNameSpaceStart(stream);

            //write class
            WriteClassStart(stream,schema);
            WriteFields(stream,schema);
            WriteClassEnd(stream,schema);

            //write name space end
            WriteNameSpaceEnd(stream);
        }

        protected  void WriteNameSpaceStart(TextWriter stream)
        {
            if (!string.IsNullOrEmpty(ns))
            {
                stream.WriteLine($"namespace {ns}");
                stream.WriteLine("{");
            }
        }
        protected void WriteNameSpaceEnd(TextWriter stream)
        {
            if (!string.IsNullOrEmpty(ns))
            {
                stream.WriteLine("}");
            }
        }

        protected void WriteClassStart(TextWriter stream, Schema schema)
        {
            if (schema!=null)
            {
                stream.WriteLine($"public class {schema.name}");
                stream.WriteLine("{");
            }
        }
        protected void WriteClassEnd(TextWriter stream, Schema schema)
        {
            if (schema != null)
            {
                stream.WriteLine("}");
            }
        }
        protected void WriteFields(TextWriter stream, Schema schema)
        {
            if (schema == null)
            {
                return;
            }
            foreach(Field field in schema.GetSideFields(side))
            {
                WriteField(stream, field);
            }
        }
        protected void WriteField(TextWriter stream, Field field)
        {
            string comment = GetFieldComment(field);
            if (!string.IsNullOrEmpty(comment))
            {
                stream.WriteLine("    /// <summary>");
                stream.WriteLine($"    /// {comment}");
                stream.WriteLine("    /// <summary>");
            }
            stream.WriteLine($"    public {GetFieldTypeDefine(field)} {field.name}");
        }

        protected string GetFieldComment(Field field)
        {
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
                    comment += "\r\n" + field.comment;
                }
            }
            return ParseLines(comment, "    ");
        }
        string ParseLines(string str, string pad)
        {
            string[] lines = str.Replace("\r\n", "\n").Split('\n');
            string ret = lines[0];
            for (int i = 1; i < lines.Length; ++i)
            {
                ret += "\r\n" + pad + "/// " + lines[i];
            }
            return ret;
        }

        protected string GetFieldTypeDefine(Field field)
        {
            if (field.type.sign == TypeInfo.Sign.Array)
            {
                return "object[]";
            }
            else if (field.type.isGeneric && field.type.genericType.sign == TypeInfo.Sign.Array)
            {
                return field.type.genericArguments[0].ToString() + "[]";
            }
            return field.type.ToString();
        }
    }
}
