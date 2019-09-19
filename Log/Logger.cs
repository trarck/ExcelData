using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log
{
    public class Logger : ILogger
    {
        List<ITarget> m_Targets;

        private void Log(string type, string content)
        {
            content = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + " : " + type + " --> " + content;
            foreach(var target in m_Targets)
            {
                target.WriteLine(content);
            }
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
