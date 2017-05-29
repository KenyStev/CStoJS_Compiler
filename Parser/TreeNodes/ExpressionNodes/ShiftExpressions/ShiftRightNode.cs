namespace Compiler.TreeNodes.Expressions.ShiftExpressions
{
    public class ShiftRightNode : ShiftExpressionNode
    {
        ShiftRightNode(){}
        public ShiftRightNode(ExpressionNode leftExpression, ExpressionNode additiveExpression) : base(leftExpression, additiveExpression)
        {
        }
    }
}