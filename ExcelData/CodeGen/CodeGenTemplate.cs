using System.IO;
using System.Text;
using TK.Excel;

namespace TK.ExcelData
{
    public class CodeGenTemplate
    {
        public string ns;
        public Side side;

        public virtual void Generate(string outputFile, Schema schema)
        {
            using (StreamWriter fs = new StreamWriter(outputFile, false, Encoding.UTF8))
            {
                Generate(fs, schema);
            }
        }

        protected virtual void Generate(TextWriter stream, Schema schema)
        {
            WriteHeader(stream);
            WriteContent(stream, schema);
            WriteFooter(stream);
        }

        protected virtual void WriteHeader(TextWriter stream)
        {

        }

        protected virtual void WriteFooter(TextWriter stream)
        {

        }

        protected virtual void WriteContent(TextWriter stream, Schema schema)
        {

        }

    }
}
