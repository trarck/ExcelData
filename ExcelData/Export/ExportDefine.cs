using System;

namespace TK.ExcelData
{
    public class ExportInfo
    {
        public DateTime lastExport;
        public string md5;
    }

    public enum ExportResult
    {
        Success,
        NotExcelFile,
        Error
    }
}
