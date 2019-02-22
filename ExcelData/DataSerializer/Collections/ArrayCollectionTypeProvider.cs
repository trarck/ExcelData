using System;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// Provides info how to serialize <see cref="Array"/> types.
    /// </summary>
    public class ArrayCollectionTypeProvider : ICollectionTypeProvider
    {
        public bool TryGetDescription(Type type, out CollectionTypeDescription description)
        {
            if (type.IsArray)
            {
                var itemType = type.GetElementType();
                description = new CollectionTypeDescription(itemType, items => FeflectionUtils.CreateArray(items, itemType));
                return true;
            }

            description = null;
            return false;
        }

        public bool TryGetDescription(PropertyInfo propertyInfo, out CollectionTypeDescription description)
        {
            return TryGetDescription(propertyInfo.PropertyType, out description);
        }
    }
}