namespace Compiler.TreeNodes.Expressions
{
    public class ConditionalOrExpressionNode : BinaryOperatorNode
    {
        private ConditionalOrExpressionNode(){}
        public ConditionalOrExpressionNode(ExpressionNode orExpression, ExpressionNode andExpression,
        Token token) : base(orExpression,andExpression,token)
        {
        }
    }
}