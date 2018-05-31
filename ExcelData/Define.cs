
namespace TK.ExcelData
{
    public enum ExcelDataType
    {
        Object,//未知
        Int,
        Float,
        String,
        Boolean,
        Array,
        Dictionary,
        List,
        Long,
        Double,
        //暂时不支持Enum，涉及动态创建Enum
        Custom=10000
    }

    public class EDConstance
    {
        public const int SchemaNameRow = 0;
        public const int SchemaDescriptionRow = 1;
        public const int SchemaDataTypeRow = 2;
        public const int SchemaDataRow = 3;
    }
}