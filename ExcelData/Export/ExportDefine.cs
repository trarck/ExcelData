using System;
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

    public enum CodeFomate
    {
        None = 0,
        CSharp = 1,
        Cpp = 2,
        Lua = 4,
        Javascript = 8
    }

    public enum DataFomate
    {
        None = 0,
        Json = 1,
        Xml = 2,
        Binary = 4,
        LuaTable = 8,
        UnityScriptable = 16
    }

}
