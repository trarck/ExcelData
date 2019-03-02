using System;
using System.Collections.Generic;

namespace TK.ExcelData
{
    public static class EDExtensionMethods
    {
        public static Type ToSystemType(this TypeInfo dataType)
        {
            Type t = null;
            switch (dataType.sign)
            {
                case TypeInfo.Sign.Int:
                    t = typeof(int);
                    break;
                case TypeInfo.Sign.Long:
                    t = typeof(long);
                    break;
                case TypeInfo.Sign.Float:
                    t = typeof(float);
                    break;
                case TypeInfo.Sign.Double:
                    t = typeof(double);
                    break;
                case TypeInfo.Sign.String:
                    t = typeof(string);
                    break;
                case TypeInfo.Sign.Boolean:
                    t = typeof(bool);
                    break;
                case TypeInfo.Sign.Object:
                    t = typeof(object);
                    break;
            }
            return t;
        }

        public static Type ToSystemType(this Field field)
        {
            if (field.type == TypeInfo.List)
            {
                return Type.GetType("System.Collections.Generic.List`1"+ "["+field.ExtTypeToSystemType().FullName+"]");
            }
            else if (field.type == TypeInfo.Dictionary)
            {
                string memberType = field.extMemberType;
                string[] splits = memberType.Split(',');

                string fullMemberType = "";
                for (int i = 0; i < splits.Length; ++i)
                {
                    TypeInfo dataType = EnumUtil.ParseEnum<TypeInfo>(splits[i], TypeInfo.String);
                    //不支持嵌套
                    fullMemberType += ((i == 0) ? "" : ",") + dataType.ToSystemType().FullName;
                }
                return Type.GetType("System.Collections.Generic.Dictionary`2" + "[" + fullMemberType + "]");
            }
            else
            {
                return field.type.ToSystemType();
            }
        }

        public static Type ExtTypeToSystemType(this Field field)
        {
            //现在还不支持复杂类型
            return null;
        }
    }
}
