using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExcelData.DataSerializer
{
    interface IDeserialization
    {
        void Visit(object value);
    }
}
