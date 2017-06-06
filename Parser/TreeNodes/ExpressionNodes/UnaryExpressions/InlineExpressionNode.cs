namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class InlineExpressionNode : UnaryExpressionNode
    {
        public UnaryExpressionNode primary;
        public InlineExpressionNode nextExpression;
        
        public InlineExpressionNode(){}

        public InlineExpressionNode(UnaryExpressionNode primary, Token token)
        {
            this.primary = primary;
            this.nextExpression = null;
            this.token = token;
        }
    }
}