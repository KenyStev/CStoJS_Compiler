namespace Compiler.TreeNodes.Expressions.AdditiveExpressions
{
    public class SubExpressionNode : AdditiveExpressionNode
    {
        SubExpressionNode(){}
        public SubExpressionNode(ExpressionNode leftExpression, 
        ExpressionNode multExpression, Token token) : base(leftExpression, multExpression,token)
        {
        }
    }
}