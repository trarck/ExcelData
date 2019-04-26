using System.CodeDom.Compiler;
using Microsoft.VisualStudio.TextTemplating;

namespace TK.ExcelData
{
    class CodeGenTemplateGenerator : TemplateGenerator, ITextTemplatingSessionHost
    {
        public CodeGenTemplateGenerator()
        {
            Refs.Add(typeof(CompilerErrorCollection).Assembly.Location);
        }

        ITextTemplatingSession m_Session = null;

        public ITextTemplatingSession Session
        {
            get
            {
                if (m_Session == null)
                {
                    m_Session = new CodeGenTemplateSession(this);
                }
                return m_Session;
            }
            set
            {

                m_Session = value;
            }
        }

        public ITextTemplatingSession CreateSession()
        {
            return Session;
        }

        //public ITextTemplatingSession Session { get; set; }

        //public ITextTemplatingSession CreateSession()
        //{
        //    return Session = new CodeGenTemplateSession(this);
        //}

        public string PreprocessTemplate(string inputFile, string inputContent, string className)
        {
            TemplateFile = inputFile;
            string classNamespace = null;
            int s = className.LastIndexOf('.');
            if (s > 0)
            {
                classNamespace = className.Substring(0, s);
                className = className.Substring(s + 1);
            }

            return Engine.PreprocessTemplate(inputContent, this, className, classNamespace, out string language, out string[] references);
        }

        public string ProcessTemplate(string inputFile, string inputContent, ref string outputFile)
        {
            TemplateFile = inputFile;
            OutputFile = outputFile;
            var result=Engine.ProcessTemplate(inputContent, this);
            outputFile = OutputFile;
            return result;
        }       
    }
}
