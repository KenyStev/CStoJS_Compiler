namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class AccessorNode : PrimaryExpressionNode
    {
        public PrimaryExpressionNode primary;
        public IdNode identifier;

        public AccessorNode(){}

        public AccessorNode(PrimaryExpressionNode primary, IdNode identifier)
        {
            this.primary = primary;
            this.identifier = identifier;
        }
    }
}