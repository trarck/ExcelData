using System;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// Provides xml element and attribute names based on type information or property information.
    /// </summary>
    public class BaseNameProvider:INameProvider
    {
        private readonly string collectionElementName;
        private readonly string collectionItemName;

        private readonly ITypeProvider typeProvider;

        public BaseNameProvider(ITypeProvider typeProvider, string collectionElementName, string collectionItemName)
        {
            if (typeProvider == null) 
                throw new ArgumentNullException("collectionProvider");
            if (collectionElementName == null) 
                throw new ArgumentNullException("collectionElementName");
            if (collectionItemName == null) 
                throw new ArgumentNullException("collectionItemName");

            this.typeProvider = typeProvider;
            this.collectionElementName = collectionElementName;
            this.collectionItemName = collectionItemName;
        }

        public NodeName GetNodeName(Type type,object obj)
        {
            var elementName = type.Name;
            var itemName = string.Empty;

            TypeDescription description;
            if (typeProvider.TryGetDescription(type, out description)  && description.Type == TypeDescription.DescriptionType.Collection)
            {
                CollectionTypeDescription collectionTypeDescription = description as CollectionTypeDescription;
                elementName = collectionTypeDescription.ElementName;
                itemName = collectionTypeDescription.ItemName;
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

            TypeDescription description;
            if (typeProvider.TryGetDescription(propertyInfo, out description) && description.Type == TypeDescription.DescriptionType.Collection)
            {
                CollectionTypeDescription collectionTypeDescription = description as CollectionTypeDescription;
                itemName = collectionTypeDescription.ItemName;
            }

            return new NodeName(elementName, itemName);
        }
    }
}