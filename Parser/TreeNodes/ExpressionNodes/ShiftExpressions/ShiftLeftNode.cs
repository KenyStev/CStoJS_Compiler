namespace Compiler.TreeNodes.Expressions.ShiftExpressions
{
    public class ShiftLeftNode : ShiftExpressionNode
    {
        ShiftLeftNode(){}
        public ShiftLeftNode(ExpressionNode leftExpression, 
        ExpressionNode additiveExpression,Token token) : base(leftExpression,additiveExpression,token)
        {
            
        }
    }
}