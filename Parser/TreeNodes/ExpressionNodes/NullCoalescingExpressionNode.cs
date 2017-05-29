namespace Compiler.TreeNodes.Expressions
{
    public class NullCoalescingExpressionNode : ExpressionNode
    {
        public ExpressionNode nullableExpression;
        public ExpressionNode rightExpression;

        private NullCoalescingExpressionNode(){}
        public NullCoalescingExpressionNode(ExpressionNode nullableExpression, ExpressionNode rightExpression)
        {
            this.nullableExpression = nullableExpression;
            this.rightExpression = rightExpression;
        }
    }
}