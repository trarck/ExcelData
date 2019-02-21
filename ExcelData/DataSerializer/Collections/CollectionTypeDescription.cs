using System;
using System.Collections;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// Represents info how to serialize collection types.
    /// </summary>
    public class CollectionTypeDescription
    {
        public CollectionTypeDescription(Type itemType, Func<IList, object> factory)
        {
            if (itemType == null) 
                throw new ArgumentNullException("itemType");
            if (factory == null) 
                throw new ArgumentNullException("factory");

            Factory = factory;
            ItemType = itemType;
        }

        /// <summary>
        /// Gets <see cref="Type"/> of item within collection.
        /// </summary>
        public Type ItemType { get; private set; }

        /// <summary>
        /// Gets function which is responsible to create collection from list of items.
        /// </summary>
        public Func<IList, object> Factory { get; private set; }
    }
}