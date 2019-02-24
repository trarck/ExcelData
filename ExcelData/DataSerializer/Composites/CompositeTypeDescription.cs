using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// Represents info how to serialize composite types.
    /// </summary>
    public class CompositeTypeDescription:TypeDescription
    {
        public CompositeTypeDescription(
            IEnumerable<PropertyInfo> properties, 
            Func<IDictionary<PropertyInfo, object>, object> factory, 
            Func<object, PropertyInfo, object> propertyGetter)
        {
            if (properties == null) 
                throw new ArgumentNullException("properties");
            if (factory == null) 
                throw new ArgumentNullException("factory");
            if (propertyGetter == null) 
                throw new ArgumentNullException("propertyGetter");

            Properties = properties;
            Factory = factory;
            PropertyGetter = propertyGetter;
        }

        /// <summary>
        /// Gets collection of properties of composite type.
        /// </summary>
        public IEnumerable<PropertyInfo> Properties { get; private set; }

        /// <summary>
        /// Gets factory function to create composite object from collection of properties.
        /// </summary>
        public Func<IDictionary<PropertyInfo, object>, object> Factory { get; private set; }

        /// <summary>
        /// Gets function to obtain property value by its <see cref="PropertyInfo"/>.
        /// </summary>
        public Func<object, PropertyInfo, object> PropertyGetter { get; private set; }
    }
}