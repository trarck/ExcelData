using System;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    public interface ITypeProvider
    {
        bool TryGetDescription(Type type, out TypeDescription description);

        bool TryGetDescription(PropertyInfo propertyInfo, out TypeDescription description);
    }
}
