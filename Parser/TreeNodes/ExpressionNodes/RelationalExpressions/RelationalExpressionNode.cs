namespace Compiler.TreeNodes.Expressions.RelationalExpressions
{
    public class RelationalExpressionNode : BinaryOperatorNode
    {
        public RelationalExpressionNode(){}

        public RelationalExpressionNode(ExpressionNode leftExpression, 
        ExpressionNode shiftExpression,Token token) : base(leftExpression,shiftExpression,token)
        {
        }
        
    }
}