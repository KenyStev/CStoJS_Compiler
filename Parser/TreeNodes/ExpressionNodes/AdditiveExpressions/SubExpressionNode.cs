namespace Compiler.TreeNodes.Expressions.AdditiveExpressions
{
    public class SubExpressionNode : AdditiveExpressionNode
    {
        SubExpressionNode(){}
        public SubExpressionNode(ExpressionNode leftExpression, ExpressionNode multExpression) : base(leftExpression, multExpression)
        {
        }
    }
}