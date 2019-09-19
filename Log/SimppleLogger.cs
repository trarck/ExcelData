using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log
{
    public class SimppleLogger:ILogger
    {
        private static readonly SimppleLogger Logg = new SimppleLogger();
        private string _className;
        private SimppleLogger()
        {

        }

        public static SimppleLogger GetLogger(string className)
        {
            Logg._className = className;
            return Logg;
        }
        public void WriteLogs(string dirName, string type, string content)
        {
            content = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + (Logg._className ?? "") + " : " + type + " --> " + content;

            string path = AppDomain.CurrentDomain.BaseDirectory;
            if (!string.IsNullOrEmpty(path))
            {
                path = AppDomain.CurrentDomain.BaseDirectory + dirName;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                if (!File.Exists(path))
                {
                    FileStream fs = File.Create(path);
                    fs.Close();
                }
                if (File.Exists(path))
                {
                    StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default);
                    sw.WriteLineAsync(content);
                    sw.Close();
                }
            }

            Console.WriteLine(content);
        }

        private void Log(string type, string content)
        {
            WriteLogs("logs", type, content);
        }

        public void Debug(string content)
        {
            Log("Debug", content);
        }

        public void Info(string content)
        {
            Log("Info", content);
        }

        public void Warn(string content)
        {
            Log("Warn", content);
        }

        public void Error(string content)
        {
            Log("Error", content);
        }

        public void Fatal(string content)
        {
            Log("Fatal", content);
        }
    }
}
