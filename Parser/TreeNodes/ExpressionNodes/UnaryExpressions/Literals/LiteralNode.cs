namespace Compiler.TreeNodes.Expressions.UnaryExpressions.Literals
{
    public class LiteralNode : PrimaryExpressionNode
    {
        public LiteralNode(){}
        public LiteralNode(Token token)
        {
            this.token = token;
        }
    }
}