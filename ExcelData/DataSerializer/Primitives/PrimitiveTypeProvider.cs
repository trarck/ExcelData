using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// The implementation of <see cref="IPrimitiveTypeProvider"/> which just contains
    /// map of <see cref="Type"/> to corresponding <see cref="IPrimitiveSerializer"/>.
    /// </summary>
    public class PrimitiveTypeProvider : IPrimitiveTypeProvider
    {
        private readonly IDictionary<Type, IPrimitiveSerializer> serializers;

        public PrimitiveTypeProvider(IDictionary<Type, IPrimitiveSerializer> serializers)
        {
            if (serializers == null) 
                throw new ArgumentNullException("serializers");

            this.serializers = serializers;
        }

        public bool TryGetDescription(Type type, out PrimitiveTypeDescription description)
        {
            IPrimitiveSerializer primitiveSerializer;
            if (serializers.TryGetValue(type, out primitiveSerializer))
            {
                description = new PrimitiveTypeDescription(primitiveSerializer);
                return true;
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