using System;
using System.Collections.Generic;
using System.IO;

namespace ExcelData.DataSerializer
{
    interface ISerialization
    {
        void Serialize(object value);
    }
}
