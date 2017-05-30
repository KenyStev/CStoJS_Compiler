namespace Compiler.TreeNodes.Expressions.ShiftExpressions
{
    public class ShiftExpressionNode : BinaryOperatorNode
    {
        public ShiftExpressionNode(){}
        public ShiftExpressionNode(ExpressionNode leftExpression, 
        ExpressionNode additiveExpression,Token token) : base(leftExpression,additiveExpression,token)
        {
        }
    }
}