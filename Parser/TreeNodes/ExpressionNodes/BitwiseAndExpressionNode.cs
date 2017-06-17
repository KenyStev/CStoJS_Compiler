namespace Compiler.TreeNodes.Expressions
{
    public class BitwiseAndExpressionNode : BinaryOperatorNode
    {
        BitwiseAndExpressionNode(){}
        public BitwiseAndExpressionNode(ExpressionNode leftExpression, ExpressionNode equalityExpression,
        Token token) : base(leftExpression,equalityExpression,token)
        {
        }
    }
}