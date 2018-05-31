using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;

namespace TK.ExcelData
{
    public abstract class DataGen
    {
        public virtual void Init()
        {

        }

        public virtual void Generate(ISheet sheet,Schema schema, string outPath)
        {

        }
    }
}