namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class AccessorNode : PrimaryExpressionNode
    {
        public PrimaryExpressionNode primary;
        public IdNode identifier;

        public AccessorNode(){}

        public AccessorNode(PrimaryExpressionNode primary, IdNode identifier,Token token) : base(token)
        {
            this.primary = primary;
            this.identifier = identifier;
        }
    }
}