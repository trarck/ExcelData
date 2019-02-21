using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// The caching decorator over <see cref="INameProvider"/>.
    /// </summary>
    internal class CachingNameProvider : INameProvider
    {
        private readonly Dictionary<Type, NodeName> cacheByType = new Dictionary<Type, NodeName>();
        private readonly Dictionary<PropertyInfo, NodeName> cacheByPropertyInfo = new Dictionary<PropertyInfo, NodeName>();

        private readonly INameProvider provider;

        public CachingNameProvider(INameProvider provider)
        {
            if (provider == null) 
                throw new ArgumentNullException("provider");

            this.provider = provider;
        }

        public NodeName GetNodeName(Type type)
        {
            if (cacheByType.ContainsKey(type))
            {
                return cacheByType[type];
            }

            var nodeName = provider.GetNodeName(type);
            cacheByType[type] = nodeName;
            return nodeName;
        }

        public NodeName GetNodeName(PropertyInfo propertyInfo)
        {
            if (cacheByPropertyInfo.ContainsKey(propertyInfo))
            {
                return cacheByPropertyInfo[propertyInfo];
            }

            var nodeName = provider.GetNodeName(propertyInfo);
            cacheByPropertyInfo[propertyInfo] = nodeName;
            return nodeName;
        }
    }
}