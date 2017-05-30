namespace Compiler.TreeNodes.Expressions
{
    public abstract class VariableInitializerNode
    {
        public Token token;

        public VariableInitializerNode(){}
        public VariableInitializerNode(Token token)
        {
            this.token = token;
        }
    }
}