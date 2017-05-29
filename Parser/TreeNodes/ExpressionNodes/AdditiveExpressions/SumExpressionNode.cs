namespace Compiler.TreeNodes.Expressions.AdditiveExpressions
{
    public class SumExpressionNode : AdditiveExpressionNode
    {
        SumExpressionNode(){}
        public SumExpressionNode(ExpressionNode leftExpression, ExpressionNode multExpression) : base(leftExpression,multExpression)
        {
        }
    }
}