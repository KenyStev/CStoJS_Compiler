namespace Compiler.TreeNodes.Expressions.RelationalExpressions
{
    public class LessThanExpressionNode : RelationalExpressionNode
    {
        public LessThanExpressionNode(){}
        public LessThanExpressionNode(ExpressionNode leftExpression, 
        ExpressionNode shiftExpression,Token token) : base(leftExpression,shiftExpression,token)
        {
            
        }
    }
}