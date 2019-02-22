using System;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// Provides xml element and attribute names based on type information or property information.
    /// </summary>
    public class NameProvider : INameProvider
    {
        private readonly string collectionElementName;
        private readonly string collectionItemName;

        private readonly ICollectionTypeProvider collectionProvider;

        public NameProvider(ICollectionTypeProvider collectionProvider, string collectionElementName, string collectionItemName)
        {
            if (collectionProvider == null) 
                throw new ArgumentNullException("collectionProvider");
            if (collectionElementName == null) 
                throw new ArgumentNullException("collectionElementName");
            if (collectionItemName == null) 
                throw new ArgumentNullException("collectionItemName");

            this.collectionProvider = collectionProvider;
            this.collectionElementName = collectionElementName;
            this.collectionItemName = collectionItemName;
        }

        public NodeName GetNodeName(Type type,object obj)
        {
            var elementName = type.Name;
            var itemName = string.Empty;

            CollectionTypeDescription collectionDescription;

            // if it is collection type
            if (collectionProvider.TryGetDescription(type, out collectionDescription))
            {
                elementName = collectionElementName;
                itemName = collectionItemName;
            }
            else if (type.IsGenericType)
            {
                // if generic type, cut of 'generic' part of type name
                elementName = type.Name.Substring(0, type.Name.IndexOf('`'));
            }

            return new NodeName(elementName, itemName);
        }

        public NodeName GetNodeName(PropertyInfo propertyInfo,object obj)
        {
            var elementName = propertyInfo.Name;
            var itemName = string.Empty;

            CollectionTypeDescription collectionDescription;

            // if type of property is collection, then additionally provide item name
            if (collectionProvider.TryGetDescription(propertyInfo.PropertyType, out collectionDescription))
            {
                itemName = collectionItemName;
            }

            return new NodeName(elementName, itemName);
        }
    }
}