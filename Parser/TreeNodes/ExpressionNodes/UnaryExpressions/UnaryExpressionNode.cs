namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class UnaryExpressionNode : ExpressionNode
    {

        public UnaryExpressionNode(){}
        public UnaryExpressionNode(Token token)
        {
            this.token = token;
        }
    }
}