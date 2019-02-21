using System;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// Checks if specified type should be serialized as primitive
    /// and provides info how it should be serialized.
    /// </summary>
    public interface IPrimitiveTypeProvider
    {
        /// <summary>
        /// Checks if specified type should be serialized as primitive
        /// and provides info how it should be serialized.
        /// </summary>
        bool TryGetDescription(Type type, out PrimitiveTypeDescription description);

        /// <summary>
        /// Checks if specified property should be serialized as primitive
        /// and provides info how it should be serialized.
        /// </summary>
        bool TryGetDescription(PropertyInfo propertyInfo, out PrimitiveTypeDescription description);
    }
}