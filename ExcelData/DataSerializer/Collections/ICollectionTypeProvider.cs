using System;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// Checks if specified type should be serialized as collection
    /// and provides info how it should be serialized.
    /// </summary>
    public interface ICollectionTypeProvider
    {
        /// <summary>
        /// Checks if specified type should be serialized as collection
        /// and provides info how it should be serialized.
        /// </summary>
        bool TryGetDescription(Type type, out CollectionTypeDescription description);

        /// <summary>
        /// Checks if specified property should be serialized as collection
        /// and provides info how it should be serialized.
        /// </summary>
        bool TryGetDescription(PropertyInfo propertyInfo, out CollectionTypeDescription description);
    }
}