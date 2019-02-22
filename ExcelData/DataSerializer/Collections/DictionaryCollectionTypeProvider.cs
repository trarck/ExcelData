using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// Provides info how to serialize IDictionary and Dictionary types.
    /// </summary>
    public class DictionaryCollectionTypeProvider : ICollectionTypeProvider
    {
        private static readonly HashSet<Type> dictionaryTypes = new HashSet<Type>
            {
                typeof(IDictionary<,>), 
                typeof(Dictionary<,>)
            };

        public bool TryGetDescription(Type type, out CollectionTypeDescription description)
        {
            if (type.IsGenericType)
            {
                var genericTypeDefinition = type.GetGenericTypeDefinition();
                if (dictionaryTypes.Contains(genericTypeDefinition))
                {
                    description = GetDescription(type);
                    return true;
                }
            }

            description = null;
            return false;
        }

        public bool TryGetDescription(PropertyInfo propertyInfo, out CollectionTypeDescription description)
        {
            return TryGetDescription(propertyInfo.PropertyType, out description);
        }

        private static CollectionTypeDescription GetDescription(Type dictionaryType)
        {
            var genericArguments = dictionaryType.GetGenericArguments();
            var itemType = typeof(KeyValuePair<,>).MakeGenericType(genericArguments);
            return new CollectionTypeDescription(itemType, items => FeflectionUtils.CreateDictionary(items, genericArguments[0], genericArguments[1]));
        }
    }
}