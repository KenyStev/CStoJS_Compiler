namespace Compiler.TreeNodes.Expressions
{
    public class BitwiseOrExpressionNode : ExpressionNode
    {
        public ExpressionNode leftExpression;
        public ExpressionNode exclusiveOrExpression;

        private BitwiseOrExpressionNode(){}
        public BitwiseOrExpressionNode(ExpressionNode leftExpression, ExpressionNode exclusiveOrExpression,
        Token token) : base(token)
        {
            this.leftExpression = leftExpression;
            this.exclusiveOrExpression = exclusiveOrExpression;
        }
    }
}