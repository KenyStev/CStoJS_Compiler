namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class UnaryNode : UnaryExpressionNode
    {
        public TokenType unaryOperator;
        public UnaryExpressionNode unaryExpression;

        public UnaryNode(){}

        public UnaryNode(TokenType unaryOperator, UnaryExpressionNode unaryExpression,Token token) : base(token)
        {
            this.unaryOperator = unaryOperator;
            this.unaryExpression = unaryExpression;
        }
    }
}