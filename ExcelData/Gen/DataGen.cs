using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;

namespace TK.ExcelData
{
    public abstract class DataGen
    {
        protected string m_ExportExt=".json";

        public virtual void Init()
        {

        }

        public virtual void Generate(ISheet sheet,Schema schema, string outPath)
        {

        }
    }
}