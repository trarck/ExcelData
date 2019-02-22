using System;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// The decorator over <see cref="INameProvider"/> which modifies 
    /// node name using specified <see cref="INameNormalizer"/>.
    /// </summary>
    public class NameNormilizerNameProvider : INameProvider
    {
        private readonly INameProvider provider;
        private readonly INameNormalizer nameNormalizer;

        public NameNormilizerNameProvider(INameProvider provider, INameNormalizer nameNormalizer)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");
            if (nameNormalizer == null)
                throw new ArgumentNullException("nameNormalizer");

            this.provider = provider;
            this.nameNormalizer = nameNormalizer;
        }

        public NodeName GetNodeName(PropertyInfo propertyInfo,object obj)
        {
            var nodeName = provider.GetNodeName(propertyInfo,obj);
            return NormalizeName(nodeName);
        }

        public NodeName GetNodeName(Type type,object obj)
        {
            var nodeName = provider.GetNodeName(type,obj);
            return NormalizeName(nodeName);
        }

        private NodeName NormalizeName(NodeName nodeName)
        {
            var elementName = nodeName.HasElementName ? nameNormalizer.NormalizeName(nodeName.ElementName) : null;
            var itemName = nodeName.HastItemName ? nameNormalizer.NormalizeName(nodeName.ItemName) : null;
            var attributeName = nodeName.HasAttributeName ? nameNormalizer.NormalizeName(nodeName.AttributeName) : null;

            return new NodeName(elementName, itemName, attributeName);
        }
    }
}