using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// The implementation of <see cref="IPropertiesProvider"/> which 
    /// returns all read/write public instance properties of type
    /// based on <see cref="DataContractAttribute"/>, <see cref="DataMemberAttribute"/> and <see cref="IgnoreDataMemberAttribute"/> attributes.
    /// </summary>
    public class DataAttributesPropertiesProvider : IPropertiesProvider
    {
        public IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            if (!type.HasAttribute<DataContractAttribute>())
            {
                throw new SerializationException(string.Format("Type '{0}' is not marked by DataContractAttribute attribute", type));
            }
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.GetProperty);
            List<PropertyInfo> result = new List<PropertyInfo>();
            foreach (var pi in properties)
            {
                if (!pi.HasAttribute<IgnoreDataMemberAttribute>())
                {
                    result.Add(pi);
                }
            }
            return result;
        }
    }
}