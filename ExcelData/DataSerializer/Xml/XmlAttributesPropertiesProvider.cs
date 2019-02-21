using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// The implementation of <see cref="IPropertiesProvider"/> which 
    /// returns all read/write public instance properties of type
    /// based on <see cref="XmlElementAttribute"/>, <see cref="XmlArrayAttribute"/> 
    /// and <see cref="XmlIgnoreAttribute"/> attributes.
    /// </summary>
    public class XmlAttributesPropertiesProvider : IPropertiesProvider
    {
        public IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            PropertyInfo[] properties=type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.GetProperty);

            List<PropertyInfo> result = new List<PropertyInfo>();
            for (int i = 0, l = properties.Length; i < l; ++i)
            {
                if (!properties[i].HasAttribute<XmlIgnoreAttribute>())
                {
                    result.Add(properties[i]);
                }
            }
            return result;
        }
    }
}