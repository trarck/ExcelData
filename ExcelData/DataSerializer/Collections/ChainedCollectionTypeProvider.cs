using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// Picks up first <see cref="ICollectionTypeProvider"/> from collection 
    /// which knows how to serialize specified collection type.
    /// </summary>
    public class ChainedCollectionTypeProvider : ICollectionTypeProvider
    {
        private readonly IEnumerable<ICollectionTypeProvider> providers;

        public ChainedCollectionTypeProvider(IEnumerable<ICollectionTypeProvider> providers)
        {
            if (providers == null)
                throw new ArgumentNullException("providers");

            this.providers = providers;
        }

        public bool TryGetDescription(Type type, out CollectionTypeDescription description)
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

        public bool TryGetDescription(PropertyInfo propertyInfo, out CollectionTypeDescription description)
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