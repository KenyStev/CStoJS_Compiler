namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public abstract class UnaryExpressionNode : ExpressionNode
    {

        public UnaryExpressionNode(){}
        public UnaryExpressionNode(Token token)
        {
            this.token = token;
        }
    }
}