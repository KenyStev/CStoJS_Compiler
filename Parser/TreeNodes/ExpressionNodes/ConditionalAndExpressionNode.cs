namespace Compiler.TreeNodes.Expressions
{
    public class ConditionalAndExpressionNode : ExpressionNode
    {
        public ExpressionNode leftExpression;
        public ExpressionNode bitsOt;

        private ConditionalAndExpressionNode(){}
        public ConditionalAndExpressionNode(ExpressionNode leftExpression, ExpressionNode bitsOt)
        {
            this.leftExpression = leftExpression;
            this.bitsOt = bitsOt;
        }
    }
}