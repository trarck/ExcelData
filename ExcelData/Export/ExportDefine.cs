﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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