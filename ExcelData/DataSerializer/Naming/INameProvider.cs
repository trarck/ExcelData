using System;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// Responsible to provide names for elements and attributes of xml document.
    /// </summary>
    public interface INameProvider
    {
        /// <summary>
        /// Provides name based on information about <see cref="PropertyInfo"/>.
        /// </summary>
        NodeName GetNodeName(PropertyInfo propertyInfo);

        /// <summary>
        /// Provides name based on information about <see cref="Type"/>.
        /// </summary>
        NodeName GetNodeName(Type type);
    }
}