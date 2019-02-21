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

        public NodeName GetNodeName(PropertyInfo propertyInfo)
        {
            var nodeName = provider.GetNodeName(propertyInfo);
            return NormalizeName(nodeName);
        }

        public NodeName GetNodeName(Type type)
        {
            var nodeName = provider.GetNodeName(type);
            return NormalizeName(nodeName);
        }

        private NodeName NormalizeName(NodeName nodeName)
        {
            var elementName = nodeName.HasElementName ? nameNormalizer.NormalizeName(nodeName.ElementName.Name) : null;
            var itemName = nodeName.HastItemName ? nameNormalizer.NormalizeName(nodeName.ItemName.Name) : null;
            var attributeName = nodeName.HasAttributeName ? nameNormalizer.NormalizeName(nodeName.AttributeName.Name) : null;

            return new NodeName(elementName, itemName, attributeName);
        }
    }
}