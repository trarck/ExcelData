namespace ExcelData.DataSerializer
{
    internal interface INodeVisitor
    {
        void Visit(PrimitiveNode node);

        void Visit(CollectionNode node);

        void Visit(CompositeNode node);
    }
}