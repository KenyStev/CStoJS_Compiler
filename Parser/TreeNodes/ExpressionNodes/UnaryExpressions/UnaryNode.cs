namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class UnaryNode : UnaryExpressionNode
    {
        public Token unaryOperator;
        public UnaryExpressionNode unaryExpression;

        public UnaryNode(){}

        public UnaryNode(Token unaryOperator, UnaryExpressionNode unaryExpression)
        {
            this.unaryOperator = unaryOperator;
            this.unaryExpression = unaryExpression;
        }
    }
}