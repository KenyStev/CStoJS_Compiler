namespace Compiler.TreeNodes.Expressions.ShiftExpressions
{
    public class ShiftExpressionNode : BinaryOperatorNode
    {
        public ExpressionNode leftExpression;
        public ExpressionNode additiveExpression;

        public ShiftExpressionNode(){}
        public ShiftExpressionNode(ExpressionNode leftExpression, ExpressionNode additiveExpression)
        {
            this.leftExpression = leftExpression;
            this.additiveExpression = additiveExpression;
        }
    }
}