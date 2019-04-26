using NPOI.SS.UserModel;
using TK.Excel;

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

        public virtual void Generate(ISheet sheet,Schema schema, HeadModel headModel,Side side, string outputFile)
        {

        }
    }
}