namespace Compiler.TreeNodes.Types
{
    public class VoidTypeNode : TypeNode
    {
        VoidTypeNode(){}
        public VoidTypeNode(Token token)
        {
            this.token = token;
        }
    }
}