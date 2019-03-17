using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;

namespace TK.ExcelData
{
    public abstract class DataGen
    {
        protected string m_ExportExt=".json";
        public virtual string exportExt
        {
            get { return m_ExportExt; }
            set
            {
                m_ExportExt = value;
            }
        }

        public virtual void Init()
        {

        }

        public virtual void Generate(ISheet sheet,Schema schema, HeadModel headModel, string outputFile)
        {

        }
    }
}