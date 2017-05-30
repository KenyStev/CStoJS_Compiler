namespace Compiler.TreeNodes.Expressions.UnaryExpressions.ReferenceAccsess
{
    public class ReferenceAccsessNode : PrimaryExpressionNode
    {

        public ReferenceAccsessNode(){}
        public ReferenceAccsessNode(Token token)
        {
            this.token = token;
        }
    }
}