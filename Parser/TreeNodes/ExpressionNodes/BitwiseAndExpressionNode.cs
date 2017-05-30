namespace Compiler.TreeNodes.Expressions
{
    public class BitwiseAndExpressionNode : ExpressionNode
    {
        public ExpressionNode leftExpression;
        public ExpressionNode equalityExpression;

         BitwiseAndExpressionNode(){}
        public BitwiseAndExpressionNode(ExpressionNode leftExpression, ExpressionNode equalityExpression,
        Token token) : base(token)
        {
            this.leftExpression = leftExpression;
            this.equalityExpression = equalityExpression;
        }
    }
}