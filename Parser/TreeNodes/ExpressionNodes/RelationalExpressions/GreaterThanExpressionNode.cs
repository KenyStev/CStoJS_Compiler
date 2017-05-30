namespace Compiler.TreeNodes.Expressions.RelationalExpressions
{
    public class GreaterThanExpressionNode : RelationalExpressionNode
    {
        public GreaterThanExpressionNode(){}
        public GreaterThanExpressionNode(ExpressionNode leftExpression, 
        ExpressionNode shiftExpression,Token token) : base(leftExpression, shiftExpression,token)
        {
        }
    }
}