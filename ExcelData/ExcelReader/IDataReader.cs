using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TK.ExcelData
{
    public interface IDataReader
    {
        List<T> ReadList<T>();
        List<object> ReadList();

        Dictionary<string, T> ReadDictionary<T>();
        Dictionary<string, object> ReadDictionary();
    }
}
