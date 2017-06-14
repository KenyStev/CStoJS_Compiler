namespace Compiler.TreeNodes.Expressions.UnaryExpressions.InstanceExpressions
{
    public class NullExpressionNode : PrimaryExpressionNode
    {
        public NullExpressionNode(){}
        public NullExpressionNode(Token token)
        {
            this.token = token;
        }
    }
}