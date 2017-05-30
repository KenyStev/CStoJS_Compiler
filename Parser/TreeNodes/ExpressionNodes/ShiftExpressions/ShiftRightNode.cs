namespace Compiler.TreeNodes.Expressions.ShiftExpressions
{
    public class ShiftRightNode : ShiftExpressionNode
    {
        ShiftRightNode(){}
        public ShiftRightNode(ExpressionNode leftExpression, 
        ExpressionNode additiveExpression,Token token) : base(leftExpression, additiveExpression,token)
        {
        }
    }
}