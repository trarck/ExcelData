using System;
using System.Collections;
using System.Collections.Generic;
using NPOI.SS.UserModel;

namespace TK.ExcelData
{
    public class DataReader : IDataReader
    {
        public Dictionary<string, object> ReadDictionary()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, T> ReadDictionary<T>()
        {
            throw new NotImplementedException();
        }

        public List<object> ReadList()
        {
            throw new NotImplementedException();
        }

        public List<T> ReadList<T>()
        {
            throw new NotImplementedException();
        }
    }
}