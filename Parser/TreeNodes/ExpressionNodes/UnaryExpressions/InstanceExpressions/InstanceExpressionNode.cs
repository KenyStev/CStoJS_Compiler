namespace Compiler.TreeNodes.Expressions.UnaryExpressions.InstanceExpressions
{
    public class InstanceExpressionNode : PrimaryExpressionNode
    {
        public InstanceExpressionNode(){}
        public InstanceExpressionNode(Token token)
        {
            this.token = token;
        }
    }
}