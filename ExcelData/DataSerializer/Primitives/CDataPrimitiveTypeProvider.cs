using System;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// The decorator over <see cref="IPrimitiveTypeProvider"/> which checks if property 
    /// is marked by <see cref="XmlElementAttribute"/> with type <see cref="XmlCDataSection"/>
    /// and replaces text data type to <see cref="XmlCharacterType.CData"/>
    /// </summary>
    public class CDataPrimitiveTypeProvider : IPrimitiveTypeProvider
    {
        private readonly IPrimitiveTypeProvider provider;

        public CDataPrimitiveTypeProvider(IPrimitiveTypeProvider provider)
        {
            if (provider == null) 
                throw new ArgumentNullException("provider");

            this.provider = provider;
        }

        public bool TryGetDescription(Type type, out PrimitiveTypeDescription description)
        {
            return provider.TryGetDescription(type, out description);
        }

        public bool TryGetDescription(PropertyInfo propertyInfo, out PrimitiveTypeDescription description)
        {
            if (provider.TryGetDescription(propertyInfo, out description))
            {
                var attr = propertyInfo.FindAttribute<XmlElementAttribute>();
                if (attr != null && attr.Type == typeof(XmlCDataSection))
                {
                    description = new PrimitiveTypeDescription(description.Serializer, XmlCharacterType.CData);
                }

                return true;
            }

            return false;
        }
    }
}