using System;

namespace ExcelData.DataSerializer
{
    internal class PrimitiveNode : INode
    {
        public PrimitiveNode(PrimitiveTypeDescription description)
        {
            if (description == null)
                throw new ArgumentNullException("description");

            Description = description;
        }

        public PrimitiveTypeDescription Description { get; private set; }

        public object Value { get; set; }

        public NodeName Name { get; set; }

        public void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public object Clone()
        {
            return new PrimitiveNode(Description);
        }
    }
}