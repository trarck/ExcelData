using TK.Excel;

namespace TK.ExcelData
{
    public class ImportSetting
    {
        private HeadModel m_HeadModel = HeadModel.CreateModel();
        public HeadModel headModel
        {
            get { return m_HeadModel; }
            set { m_HeadModel = value; }
        }
    }
}
