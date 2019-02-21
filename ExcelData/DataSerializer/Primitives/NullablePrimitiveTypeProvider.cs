using System;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// Provides info how to serialize nullable types. Uses specified <see cref="IPrimitiveTypeProvider"/>
    /// to provide info how to serialize underlying type of nullable type.
    /// </summary>
    public class NullablePrimitiveTypeProvider : IPrimitiveTypeProvider
    {
        private readonly IPrimitiveTypeProvider provider;

        public NullablePrimitiveTypeProvider(IPrimitiveTypeProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            this.provider = provider;
        }

        public bool TryGetDescription(Type type, out PrimitiveTypeDescription description)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return provider.TryGetDescription(type.GetGenericArguments()[0], out description);
            }

            description = null;
            return false;
        }

        public bool TryGetDescription(PropertyInfo propertyInfo, out PrimitiveTypeDescription description)
        {
            return TryGetDescription(propertyInfo.PropertyType, out description);
        }
    }
}