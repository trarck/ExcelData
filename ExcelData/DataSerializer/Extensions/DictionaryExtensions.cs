using System;
using System.Collections.Generic;

namespace ExcelData.DataSerializer
{
    internal static class DictionaryExtensions
    {
        /// <summary>
        /// Adds to dictionary all entries from specified dictionary not presented in dictionary.
        /// </summary>
        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IDictionary<TKey, TValue> source)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");
            if (source == null)
                throw new ArgumentNullException("source");

            foreach (var key in source.Keys)
            {
                if (!dictionary.ContainsKey(key))
                {
                    dictionary[key] = source[key];
                }
            }

            return dictionary;
        }
    }
}