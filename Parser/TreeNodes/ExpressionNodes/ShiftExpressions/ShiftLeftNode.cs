namespace Compiler.TreeNodes.Expressions.ShiftExpressions
{
    public class ShiftLeftNode : ShiftExpressionNode
    {
        ShiftLeftNode(){}
        public ShiftLeftNode(ExpressionNode leftExpression, ExpressionNode additiveExpression) : base(leftExpression,additiveExpression)
        {
            
        }
    }
}