using Microsoft.VisualStudio.TextTemplating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TK.ExcelData
{
    public class ClassCodeGen
    {
        CodeGenTemplateGenerator m_Generator = null;
   
        public string inputFile { get; set; }
        public string preprocessClassName { get; set; }

        public Schema schema { get; set; }
        public string ns { get; set; }

        public void Init()
        {
            m_Generator =new CodeGenTemplateGenerator();
            m_Generator.Refs.Add(Assembly.GetExecutingAssembly().Location);
            m_Generator.Refs.Add(typeof(Enumerable).Assembly.Location);
        }

        public void SynicSession()
        {
            AddSession("ns", ns);
            AddSession("schema", schema);
        }

        public void Generate(Schema schema,string outputFile,string ns=null)
        {
            AddSession("ns", ns);
            AddSession("schema", schema);

            Generate(outputFile);
        }

        public void Generate( string outputFile)
        {
            string inputContent = null;
            string outputContent = null;

            if (inputFile != null)
            {
                try
                {
                    inputContent = File.ReadAllText(inputFile);
                }
                catch (IOException ex)
                {
                    throw new Exception("Could not read input file '" + inputFile + "':\n" + ex);
                }
            }

            if (inputContent.Length == 0)
            {
                throw new Exception("Input is empty");
            }

            if (!m_Generator.Errors.HasErrors)
            {
                if (preprocessClassName == null)
                {
                    outputContent = m_Generator.ProcessTemplate(inputFile, inputContent, ref outputFile);
                }
                else
                {
                    outputContent = m_Generator.PreprocessTemplate(inputFile, inputContent, preprocessClassName);
                }
            }

            if (m_Generator.Errors.HasErrors)
            {
                throw new Exception(inputFile == null ? "Processing failed." : $"Processing '{inputFile}' failed.");
            }

            try
            {
                if (!m_Generator.Errors.HasErrors)
                {
                    //check file directory exists
                    string folder = Path.GetDirectoryName(outputFile);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    File.WriteAllText(outputFile, outputContent, Encoding.UTF8);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Could not write output file '" + outputFile + "':\n" + ex);
            }

            LogErrors(m_Generator);
        }


        public void AddSession(string key,object value)
        {
            if (m_Generator == null) return;
            m_Generator.Session[key] = value;
        }

        public bool AddDirectiveProcessors( List<string> directives)
        {
            if (m_Generator == null) return false;

            foreach (var dir in directives)
            {
                var split = dir.Split('!');

                if (split.Length != 3)
                {
                    Console.Error.WriteLine("Directive must have 3 values: {0}", dir);
                    return false;
                }

                for (int i = 0; i < 3; i++)
                {
                    string s = split[i];
                    if (string.IsNullOrEmpty(s))
                    {
                        string kind = i == 0 ? "name" : (i == 1 ? "class" : "assembly");
                        Console.Error.WriteLine("Directive has missing {0} value: {1}", kind, dir);
                        return false;
                    }
                }

                m_Generator.AddDirectiveProcessor(split[0], split[1], split[2]);
            }
            return true;
        }

        static void LogErrors(TemplateGenerator generator)
        {
            foreach (System.CodeDom.Compiler.CompilerError err in generator.Errors)
            {
                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = err.IsWarning ? ConsoleColor.Yellow : ConsoleColor.Red;
                if (!string.IsNullOrEmpty(err.FileName))
                {
                    Console.Error.Write(err.FileName);
                }
                if (err.Line > 0)
                {
                    Console.Error.Write("(");
                    Console.Error.Write(err.Line);
                    if (err.Column > 0)
                    {
                        Console.Error.Write(",");
                        Console.Error.Write(err.Column);
                    }
                    Console.Error.Write(")");
                }
                if (!string.IsNullOrEmpty(err.FileName) || err.Line > 0)
                {
                    Console.Error.Write(": ");
                }
                Console.Error.Write(err.IsWarning ? "WARNING: " : "ERROR: ");
                Console.Error.WriteLine(err.ErrorText);
                Console.ForegroundColor = oldColor;
            }
        }
    }
}
