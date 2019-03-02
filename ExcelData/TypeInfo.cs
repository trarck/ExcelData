using System;
using System.Collections.Generic;

namespace TK.ExcelData
{
    
    public class TypeInfo
    {
        public TypeInfo(string name)
        {
            this.name = name;
        }

        public TypeInfo(string name, TypeInfo genericType)
        {
            this.name = name;
            isGeneric = true;
            this.genericType = genericType;
        }

        public TypeInfo(string name, TypeInfo genericType,TypeInfo genericKeyType)
        {
            this.name = name;
            isGeneric = true;
            this.genericType = genericType;
            this.genericKeyType = genericKeyType;
        }

        public string name { get; set; }
        public bool isGeneric { get; set; }
        public TypeInfo genericType { get; set; }
        public TypeInfo genericKeyType { get; set; }

        public bool isGenericArray { get { return isGeneric && genericKeyType == null; } }
        public bool isGenericDictionary { get { return isGeneric && genericKeyType != null; } }

        public static TypeInfo Int = new TypeInfo("int");
        public static TypeInfo Long = new TypeInfo("Long");
        public static TypeInfo Float = new TypeInfo("Float");
        public static TypeInfo Double = new TypeInfo("Double");
        public static TypeInfo String = new TypeInfo("String");
        public static TypeInfo Boolean = new TypeInfo("Boolean");
        public static TypeInfo Object = new TypeInfo("Object");
        public static TypeInfo Array = new TypeInfo("Array");
        public static TypeInfo List = new TypeInfo("List");
        public static TypeInfo Dictionary = new TypeInfo("Dictionary");

        public static TypeInfo Parse(string type)
        {
            type = type.Trim();
            //primitive type
            switch (type)
            {
                case "int":
                case "Int":
                    return TypeInfo.Int;
                case "float":
                case "Float":
                    return TypeInfo.Float;
                case "string":
                case "String":
                    return TypeInfo.String;
                case "bool":
                case "Bool":
                    return TypeInfo.Boolean;
                case "long":
                case "Long":
                    return TypeInfo.Long;
                case "double":
                case "Double":
                    return TypeInfo.Double;
                case "array":
                case "Array":
                    return TypeInfo.Array;
                case "list":
                case "List":
                    return TypeInfo.List;
                case "dictionary":
                case "Dictionary":
                    return TypeInfo.Dictionary;
            }

            //genetic type
            int pos = type.IndexOf("<");
            if (pos > -1)
            {
                string baseType = type.Substring(0, pos);
                int posEnd = type.LastIndexOf(">");

                string geneticType = type.Substring(pos + 1, posEnd - pos - 1);

                pos = geneticType.IndexOf(",");
                if (pos > -1)
                {
                    string keyType = geneticType.Substring(0, pos);
                    if (keyType.IndexOf("<") == -1)
                    {
                        return new TypeInfo(type, Parse(geneticType), Parse(geneticType.Substring(pos+1)));
                    }
                    else
                    {
                        //list is generic dictionary
                        return new TypeInfo(type, Parse(geneticType));
                    }
                }
                else
                {
                    return new TypeInfo(type, Parse(geneticType));
                }
            }
            return TypeInfo.Object;
        }
    }
}