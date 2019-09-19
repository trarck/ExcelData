using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log
{
    public interface ITarget
    {
        void Init();

        void Write(string content);
        void WriteLine(string content);
    }
}
