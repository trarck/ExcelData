using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

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
