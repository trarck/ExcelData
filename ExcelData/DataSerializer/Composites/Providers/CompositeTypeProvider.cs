using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// The implementation of <see cref="ICompositeTypeProvider"/> which uses 
    /// specified <see cref="IPropertiesProvider"/> to get properties of composite object
    /// and parameterless constructor (if exists) to create instances of composite types.
    /// </summary>
    public class CompositeTypeProvider : ICompositeTypeProvider
    {
        private readonly IPropertiesProvider propertiesProvider;

        public CompositeTypeProvider(IPropertiesProvider propertiesProvider)
        {
            if (propertiesProvider == null)
                throw new ArgumentNullException("propertiesProvider");

            this.propertiesProvider = propertiesProvider;
        }

        public bool TryGetDescription(Type type, out CompositeTypeDescription description)
        {
            var properties = propertiesProvider.GetProperties(type);
            Func<object> ctorFunc = () => {
                return Activator.CreateInstance(type);
            };

            description = new CompositeTypeDescription(properties, ps => CreateObject(ctorFunc, ps), (obj, pi) => {
                return pi.GetValue(obj,null);
            });
            return true;
        }

        public bool TryGetDescription(PropertyInfo propertyInfo, out CompositeTypeDescription description)
        {
            return TryGetDescription(propertyInfo.PropertyType, out description);
        }

        private static object CreateObject(Func<object> ctor, IDictionary<PropertyInfo, object> properties)
        {
            var value = ctor();
            foreach (var propertyInfo in properties.Keys)
            {
                propertyInfo.SetValue(value, properties[propertyInfo], null);
            }

            return value;
        }
    }
}