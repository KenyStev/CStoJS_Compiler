namespace Compiler.TreeNodes.Expressions.EqualityExpressions
{
    public class EqualExpressionNode : EqualityExpressionNode
    {
        EqualExpressionNode(){}
        public EqualExpressionNode(ExpressionNode leftExpression, 
        ExpressionNode relationalExpression,Token token) : base(leftExpression,relationalExpression,token)
        {
        }
    }
}