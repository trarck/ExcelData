using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// Provides info how to serialize generic IEnumerable, ICollection, Collection, IList and List types.
    /// </summary>
    public class CollectionTypeProvider : ICollectionTypeProvider
    {
        private static readonly HashSet<Type> collectionTypes = new HashSet<Type>
            {
                typeof(IEnumerable<>),
                typeof(ICollection<>),
                typeof(Collection<>),
                typeof(IList<>),
                typeof(List<>)
            };

        public bool TryGetDescription(Type type, out CollectionTypeDescription description)
        {
            if (type.IsGenericType)
            {
                var genericTypeDefinition = type.GetGenericTypeDefinition();
                if (collectionTypes.Contains(genericTypeDefinition))
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

        private static CollectionTypeDescription GetDescription(Type collectionType)
        {
            var itemType = collectionType.GetGenericArguments()[0];
            return new CollectionTypeDescription(itemType, items => FeflectionUtils.CreateList(items, itemType));
        }
    }
}