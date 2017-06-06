namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class PostAdditiveExpressionNode : PrimaryExpressionNode
    {
        public PrimaryExpressionNode primary;
        public TokenType type;

        public PostAdditiveExpressionNode(){}

        public PostAdditiveExpressionNode(TokenType type,Token token) : base(token)
        {
            this.primary = null;
            this.type = type;
        }
    }
}