namespace Compiler.TreeNodes.Expressions
{
    public class ExclusiveOrExpression : ExpressionNode
    {
        public ExpressionNode leftExpression;
        public ExpressionNode bitsAnd;

        private ExclusiveOrExpression(){}
        public ExclusiveOrExpression(ExpressionNode leftExpression, ExpressionNode bitsAnd)
        {
            this.leftExpression = leftExpression;
            this.bitsAnd = bitsAnd;
        }
    }
}