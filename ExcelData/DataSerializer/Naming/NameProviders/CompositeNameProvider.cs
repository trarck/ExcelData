using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// The implementation of <see cref="INameProvider"/> which collects and merges
    /// <see cref="NodeName"/> from collection of <see cref="INameProvider"/>. 
    /// Provider which goes first gets precedence.
    /// </summary>
    public class CompositeNameProvider : INameProvider
    {
        private readonly IEnumerable<INameProvider> providers;

        public CompositeNameProvider(IEnumerable<INameProvider> providers)
        {
            if (providers == null)
                throw new ArgumentNullException("providers");

            this.providers = providers;
        }

        public NodeName GetNodeName(Type type, object obj)
        {
            var nodeName = NodeName.Empty;
            foreach (var provider in providers)
            {
                nodeName = Merge(nodeName, provider.GetNodeName(type,obj));
            }

            return nodeName;
        }

        public NodeName GetNodeName(PropertyInfo propertyInfo, object obj)
        {
            var nodeName = NodeName.Empty;
            foreach (var provider in providers)
            {
                nodeName = Merge(nodeName, provider.GetNodeName(propertyInfo,obj));
            }

            return nodeName;
        }

        private static NodeName Merge(NodeName left, NodeName right)
        {
            var elementName = left.HasElementName ? left.ElementName : right.ElementName;
            var attributeName = left.HasAttributeName ? left.AttributeName : right.AttributeName;
            var itemName = left.HastItemName ? left.ItemName : right.ItemName;

            return new NodeName(elementName, itemName, attributeName);
        }
    }
}