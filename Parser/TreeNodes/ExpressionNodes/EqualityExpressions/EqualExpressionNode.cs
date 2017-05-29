namespace Compiler.TreeNodes.Expressions.EqualityExpressions
{
    public class EqualExpressionNode : EqualityExpressionNode
    {
        EqualExpressionNode(){}
        public EqualExpressionNode(ExpressionNode leftExpression, ExpressionNode relationalExpression) : base(leftExpression,relationalExpression)
        {
        }
    }
}