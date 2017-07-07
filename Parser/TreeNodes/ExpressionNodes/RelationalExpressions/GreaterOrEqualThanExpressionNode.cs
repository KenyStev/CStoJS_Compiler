namespace Compiler.TreeNodes.Expressions.RelationalExpressions
{
    public class GreaterOrEqualThanExpressionNode : RelationalExpressionNode
    {
        public GreaterOrEqualThanExpressionNode(){}
        public GreaterOrEqualThanExpressionNode(ExpressionNode leftExpression, 
        ExpressionNode shiftExpression,Token token) : base(leftExpression, shiftExpression,token)
        {
        }
    }
}