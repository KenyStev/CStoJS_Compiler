namespace Compiler.TreeNodes.Expressions.RelationalExpressions
{
    public class LessOrEqualThanExpressionNode : RelationalExpressionNode
    {
        public LessOrEqualThanExpressionNode(){}
        public LessOrEqualThanExpressionNode(ExpressionNode leftExpression, 
        ExpressionNode shiftExpression,Token token) : base(leftExpression, shiftExpression,token)
        {
        }
    }
}