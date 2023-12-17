
namespace TK.ExcelData
{
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
        Binary = 1,
        csv = 2,
        Json = 4,
        Xml = 8,
        LuaTable = 16,
        UnityScriptable = 32
    }
}