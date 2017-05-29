namespace Compiler.TreeNodes.Expressions
{
    public class BitwiseAndExpressionNode : ExpressionNode
    {
        public ExpressionNode leftExpression;
        public ExpressionNode equalityExpression;

         BitwiseAndExpressionNode(){}
        public BitwiseAndExpressionNode(ExpressionNode leftExpression, ExpressionNode equalityExpression)
        {
            this.leftExpression = leftExpression;
            this.equalityExpression = equalityExpression;
        }
    }
}