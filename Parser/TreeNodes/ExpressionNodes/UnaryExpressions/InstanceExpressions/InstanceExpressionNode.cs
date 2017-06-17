namespace Compiler.TreeNodes.Expressions.UnaryExpressions.InstanceExpressions
{
    public abstract class InstanceExpressionNode : PrimaryExpressionNode
    {
        public InstanceExpressionNode(){}
        public InstanceExpressionNode(Token token)
        {
            this.token = token;
        }
    }
}