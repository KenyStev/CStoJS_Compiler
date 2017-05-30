namespace Compiler.TreeNodes.Expressions
{
    public class ConditionalAndExpressionNode : ExpressionNode
    {
        public ExpressionNode leftExpression;
        public ExpressionNode bitsOt;

        private ConditionalAndExpressionNode(){}
        public ConditionalAndExpressionNode(ExpressionNode leftExpression, ExpressionNode bitsOt,
        Token token) : base(token)
        {
            this.leftExpression = leftExpression;
            this.bitsOt = bitsOt;
        }
    }
}