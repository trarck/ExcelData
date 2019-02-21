using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// Picks up first <see cref="IPrimitiveTypeProvider"/> from collection 
    /// which knows how to serialize specified primitive type.
    /// </summary>
    public class ChainedPrimitiveTypeProvider : IPrimitiveTypeProvider
    {
        private readonly IEnumerable<IPrimitiveTypeProvider> providers;

        public ChainedPrimitiveTypeProvider(IEnumerable<IPrimitiveTypeProvider> providers)
        {
            if (providers == null) 
                throw new ArgumentNullException("providers");

            this.providers = providers;
        }

        public bool TryGetDescription(Type type, out PrimitiveTypeDescription description)
        {
            foreach (var provider in providers)
            {
                if (provider.TryGetDescription(type, out description))
                {
                    return true;
                }
            }

            description = null;
            return false;
        }

        public bool TryGetDescription(PropertyInfo propertyInfo, out PrimitiveTypeDescription description)
        {
            foreach (var provider in providers)
            {
                if (provider.TryGetDescription(propertyInfo, out description))
                {
                    return true;
                }
            }

            description = null;
            return false;
        }
    }
}