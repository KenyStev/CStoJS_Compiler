namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class PostAdditiveExpressionNode : PrimaryExpressionNode
    {
        public PrimaryExpressionNode primary;
        public TokenType type;

        public PostAdditiveExpressionNode(){}

        public PostAdditiveExpressionNode(PrimaryExpressionNode primary, TokenType type,Token token) : base(token)
        {
            this.primary = primary;
            this.type = type;
        }
    }
}