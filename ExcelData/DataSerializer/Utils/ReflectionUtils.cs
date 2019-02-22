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
    }
}