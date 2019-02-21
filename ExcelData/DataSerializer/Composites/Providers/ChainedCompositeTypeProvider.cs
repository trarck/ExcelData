using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// Picks up first <see cref="ICompositeTypeProvider"/> from collection 
    /// which knows how to serialize specified composite type.
    /// </summary>
    public class ChainedCompositeTypeProvider : ICompositeTypeProvider
    {
        private readonly IEnumerable<ICompositeTypeProvider> providers;

        public ChainedCompositeTypeProvider(IEnumerable<ICompositeTypeProvider> providers)
        {
            if (providers == null) 
                throw new ArgumentNullException("providers");

            this.providers = providers;
        }

        public bool TryGetDescription(Type type, out CompositeTypeDescription description)
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

        public bool TryGetDescription(PropertyInfo propertyInfo, out CompositeTypeDescription description)
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