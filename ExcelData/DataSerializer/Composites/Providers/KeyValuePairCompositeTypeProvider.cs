using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// Provides info how to serialize KeyValuePair type.
    /// </summary>
    public class KeyValuePairCompositeTypeProvider : ICompositeTypeProvider
    {
        private readonly IPropertiesProvider propertiesProvider = new PublicPropertiesProvider();

        public bool TryGetDescription(Type type, out CompositeTypeDescription description)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
            {
                var genericArguments = type.GetGenericArguments();
                var ctor = type.GetConstructor(genericArguments);

                var properties = propertiesProvider.GetProperties(type);
                description = new CompositeTypeDescription(properties, ps => CreateObject(ctor, ps), (obj, pi) => {
                    return pi.GetValue(obj, null);
                });
                return true;
            }

            description = null;
            return false;
        }

        public bool TryGetDescription(PropertyInfo propertyInfo, out CompositeTypeDescription description)
        {
            return TryGetDescription(propertyInfo.PropertyType, out description);
        }

        private static object CreateObject(ConstructorInfo ctor, IDictionary<PropertyInfo, object> properties)
        {
            List<object> args = new List<object>();
            foreach(var iter in properties.Values)
            {
                args.Add(iter);
            }
            return ctor.Invoke(args.ToArray());
        }
    }
}