using System;

namespace ExcelData.DataSerializer
{
    internal class CompositeNode : INode
    {
        public CompositeNode(CompositeTypeDescription description)
        {
            if (description == null)
                throw new ArgumentNullException("description");

            Description = description;
        }

        public CompositeTypeDescription Description { get; private set; }

        public object Value { get; set; }

        public NodeName Name { get; set; }

        public void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public object Clone()
        {
            return new CompositeNode(Description);
        }
    }
}