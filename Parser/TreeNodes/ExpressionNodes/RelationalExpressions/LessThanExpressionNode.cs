namespace Compiler.TreeNodes.Expressions.RelationalExpressions
{
    public class LessThanExpressionNode : RelationalExpressionNode
    {
        public LessThanExpressionNode(){}
        public LessThanExpressionNode(ExpressionNode leftExpression, ExpressionNode shiftExpression) : base(leftExpression,shiftExpression)
        {
            
        }
    }
}