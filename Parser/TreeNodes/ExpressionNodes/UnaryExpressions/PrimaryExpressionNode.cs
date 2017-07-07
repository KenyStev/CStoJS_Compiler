namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public abstract class PrimaryExpressionNode : UnaryExpressionNode
    {
        public PrimaryExpressionNode(){}
        public PrimaryExpressionNode(Token token) : base(token){}
    }
}