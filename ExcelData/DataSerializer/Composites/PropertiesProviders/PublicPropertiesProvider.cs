using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// The implementation of <see cref="IPropertiesProvider"/> which 
    /// returns all read/write public instance properties of type.
    /// </summary>
    public class PublicPropertiesProvider : IPropertiesProvider
    {
        public IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | 
                BindingFlags.SetProperty | BindingFlags.GetProperty);
        }
    }
}