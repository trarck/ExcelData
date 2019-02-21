using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// Returns properties of first <see cref="IPropertiesProvider"/> 
    /// which returns not empty list of properties.
    /// </summary>
    public class ChainedPropertiesProvider : IPropertiesProvider
    {
        private readonly IEnumerable<IPropertiesProvider> providers;

        public ChainedPropertiesProvider(IEnumerable<IPropertiesProvider> providers)
        {
            if (providers == null) 
                throw new ArgumentNullException("providers");

            this.providers = providers;
        }

        public IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            foreach (var provider in providers)
            {
                var properties = provider.GetProperties(type);
                if (properties != null)
                {
                    foreach (var iter in properties)
                    {
                        if (iter != null)
                        {
                            return properties;
                        }
                    }
                }
            }

            return null;
        }
    }
}