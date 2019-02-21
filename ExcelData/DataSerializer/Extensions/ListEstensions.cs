using System;
using System.Collections;

namespace ExcelData.DataSerializer
{
    internal static class ListEstensions
    {
        public static void AddRange(this IList list, IEnumerable items)
        {
            if (list == null) 
                throw new ArgumentNullException("list");
            if (items == null) 
                throw new ArgumentNullException("items");

            foreach (var item in items)
            {
                list.Add(item);
            }
        }
    }
}