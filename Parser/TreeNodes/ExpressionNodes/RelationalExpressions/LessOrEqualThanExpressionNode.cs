namespace Compiler.TreeNodes.Expressions.RelationalExpressions
{
    public class LessOrEqualThanExpressionNode : RelationalExpressionNode
    {
        public LessOrEqualThanExpressionNode(){}
        public LessOrEqualThanExpressionNode(ExpressionNode leftExpression, ExpressionNode shiftExpression) : base(leftExpression, shiftExpression)
        {
        }
    }
}