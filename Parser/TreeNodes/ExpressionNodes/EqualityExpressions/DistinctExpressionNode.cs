namespace Compiler.TreeNodes.Expressions.EqualityExpressions
{
    public class DistinctExpressionNode : EqualityExpressionNode
    {
        DistinctExpressionNode(){}
        public DistinctExpressionNode(ExpressionNode leftExpression, 
        ExpressionNode relationalExpression,Token token) : base(leftExpression, relationalExpression,token)
        {
        }
    }
}