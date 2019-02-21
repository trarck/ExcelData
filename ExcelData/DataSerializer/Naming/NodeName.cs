namespace ExcelData.DataSerializer
{
    /// <summary>
    /// Represents name (element or attribute name) of node of object graph in xml document.
    /// </summary>
    public class NodeName
    {
        public NodeName(string elementName)
            : this(elementName, null, null)
        {
        }

        public NodeName(string elementName, string itemName)
            : this(elementName, itemName, null)
        {
        }

        public NodeName(string elementName, string itemName, string attributeName)
        {
            ElementName = GetElementName(elementName);
            ItemName = GetElementName(itemName);
            AttributeName = GetElementName(attributeName);
        }

        /// <summary>
        /// Gets xml element name for property.
        /// </summary>
        public string ElementName { get; private set; }

        /// <summary>
        /// Gets xml element name for item within collection.
        /// </summary>
        public string ItemName { get; private set; }

        /// <summary>
        /// Gets xml attribute name for property.
        /// </summary>
        public string AttributeName { get; private set; }

        /// <summary>
        /// Indicates if <see cref="ElementName"/> is not empty.
        /// </summary>
        public bool HasElementName
        {
            get { return ElementName != null; }
        }

        /// <summary>
        /// Indicates if <see cref="AttributeName"/> is not empty.
        /// </summary>
        public bool HasAttributeName
        {
            get { return AttributeName != null ; }
        }

        /// <summary>
        /// Indicates if <see cref="ItemName"/> is not empty.
        /// </summary>
        public bool HastItemName
        {
            get { return ItemName != null; }
        }

        public static NodeName Empty
        {
            get { return new NodeName(null); }
        }

        public override string ToString()
        {
            return string.Format("ElementName: {0}, AttributeName: {1}, ItemName: {2}", ElementName, AttributeName, ItemName);
        }

        private static string GetElementName(string name)
        {
            return !string.IsNullOrEmpty(name) ?  name : null;
        }
    }
}