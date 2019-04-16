using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TK.ExcelData
{
    public class ImportInfo
    {
        public DateTime lastExport;
        public string md5;
    }

    public enum ImportResult
    {
        Success,
        NotExcelFile,
        NotSheet,
        Error
    }
}
