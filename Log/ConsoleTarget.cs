using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log
{
    public class ConsoleTarget:ITarget
    {
        public ConsoleTarget()
        {
        }

        public void Init()
        {
        }

        public void Write(string content)
        {
            Console.Write(content);
        }
        public void WriteLine(string content)
        {
            Console.WriteLine(content);
        }
    }
}
