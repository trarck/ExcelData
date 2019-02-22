using System;
using System.Collections;
using System.Collections.Generic;

namespace ExcelData.DataSerializer
{
    internal static class FeflectionUtils
    {
        public static IList CreateList(IEnumerable items, Type itemType)
        {
            var value = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(new[] { itemType }));
            value.AddRange(items);
            return value;
        }

        public static Array CreateArray(ICollection items, Type itemType)
        {
            var value = Array.CreateInstance(itemType, items.Count);
            items.CopyTo(value, 0);
            return value;
        }

        public static IDictionary CreateDictionary(IEnumerable items, Type keyType, Type valueType)
        {
            var genericArguments = new[] { keyType, valueType };
            var itemType = typeof(KeyValuePair<,>).MakeGenericType(genericArguments);
            var type = typeof(Dictionary<,>).MakeGenericType(genericArguments);
            var collectionType = typeof(ICollection<>).MakeGenericType(itemType);
            var value = (IDictionary)Activator.CreateInstance(type);
            var add = collectionType.GetMethod("Add", new[] { itemType });
            foreach (var item in items)
            {
                add.Invoke(value, new[] { item });
            }

            return value;
        }

        public static HashSet<Type> PrimitiveTypes = new HashSet <Type> { typeof(char),typeof(string),
            typeof(short), typeof(ushort),
            typeof(byte),  typeof(sbyte),
            typeof(int), typeof(uint),
            typeof(long), typeof(ulong),
            typeof(float),  typeof(double),  typeof(decimal),  typeof(bool), typeof(TimeSpan), typeof(DateTime), typeof(DateTimeOffset), typeof(Uri), typeof(Guid),  typeof(Type)};
        public static bool IsPrimitive(Type type)
        {
            return PrimitiveTypes.Contains(type);
        }

        public static bool IsNumericType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}