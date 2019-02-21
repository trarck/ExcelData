using System;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// Represents node of object graph.
    /// </summary>
    internal interface INode : ICloneable
    {
        /// <summary>
        /// The value of node in object graph.
        /// </summary>
        object Value { get; set; }

        /// <summary>
        /// The name of node in xml document.
        /// </summary>
        NodeName Name { get; set; }

        void Accept(INodeVisitor visitor);
    }
}