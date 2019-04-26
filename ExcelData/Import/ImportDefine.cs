using System;

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
