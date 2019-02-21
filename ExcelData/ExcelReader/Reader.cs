using System.Collections.Generic;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

namespace TK.ExcelData
{
    public class Reader
    {
        IWorkbook m_Workbook;

        int m_SchemaNameRow = 0;
        int m_SchemaDataTypeRow = 1;

        public Reader(IWorkbook workbook)
        {
            m_Workbook = workbook;
        }

        public List<object> ReadList(ISheet sheet)
        {
            Schema schema = SchemaReader.ReadSchema(sheet);
            DataReader reader = new DataReader();

            return DataReader.ReadList(sheet, schema);
        }

        public IWorkbook workbook
        {
            set
            {
                m_Workbook = value;
            }

            get
            {
                return m_Workbook;
            }
        }

        public int schemaNameRow
        {
            set
            {
                m_SchemaNameRow = value;
            }

            get
            {
                return m_SchemaNameRow;
            }
        }

        public int schemaDataTypeRow
        {
            set
            {
                m_SchemaDataTypeRow = value;
            }

            get
            {
                return m_SchemaDataTypeRow;
            }
        }
    }
}