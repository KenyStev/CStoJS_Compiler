namespace Compiler.TreeNodes.Expressions
{
    public class ConditionalOrExpressionNode : ExpressionNode
    {
        public ExpressionNode orExpression;
        public ExpressionNode andExpression;
        private ConditionalOrExpressionNode(){}
        public ConditionalOrExpressionNode(ExpressionNode orExpression, ExpressionNode andExpression,
        Token token) : base(token)
        {
            this.orExpression = orExpression;
            this.andExpression = andExpression;
        }
    }
}