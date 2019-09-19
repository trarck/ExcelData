using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log
{
    public interface ILogger
    {
        void Debug(string content);

        void Info(string content);

        void Warn(string content);

        void Error(string content);

        void Fatal(string content);
    }
}
