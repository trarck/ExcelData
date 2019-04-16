
namespace TK.ExcelData
{
    public class Constance
    {
        public const int SchemaNameRow = 0;
        public const int SchemaDataTypeRow = 1;
        public const int SchemaDescriptionRow = 2;
        public const int SchemaDataRow = 3;
        
    }
    public enum CodeFomat
    {
        None = 0,
        CSharp = 1,
        Cpp = 2,
        Lua = 4,
        Javascript = 8
    }

    public enum DataFormat
    {
        None = 0,
        Json = 1,
        Xml = 2,
        Binary = 4,
        LuaTable = 8,
        UnityScriptable = 16
    }
}