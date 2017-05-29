namespace Compiler.TreeNodes.Expressions.EqualityExpressions
{
    public class EqualityExpressionNode : BinaryOperatorNode
    {
        public EqualityExpressionNode(){}
        public EqualityExpressionNode(ExpressionNode leftExpression, ExpressionNode relationalExpression) : base(leftExpression,relationalExpression)
        {}
    }
}