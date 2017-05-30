namespace Compiler.TreeNodes.Expressions
{
    public class NullCoalescingExpressionNode : ExpressionNode
    {
        public ExpressionNode nullableExpression;
        public ExpressionNode rightExpression;

        private NullCoalescingExpressionNode(){}
        public NullCoalescingExpressionNode(ExpressionNode nullableExpression, 
        ExpressionNode rightExpression,Token token) : base(token)
        {
            this.nullableExpression = nullableExpression;
            this.rightExpression = rightExpression;
        }
    }
}