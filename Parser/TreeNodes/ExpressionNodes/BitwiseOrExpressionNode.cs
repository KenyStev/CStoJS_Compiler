namespace Compiler.TreeNodes.Expressions
{
    public class BitwiseOrExpressionNode : BinaryOperatorNode
    {
        private BitwiseOrExpressionNode(){}
        public BitwiseOrExpressionNode(ExpressionNode leftExpression, ExpressionNode exclusiveOrExpression,
        Token token) :  base(leftExpression,exclusiveOrExpression,token)
        {
        }
    }
}