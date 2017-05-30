namespace Compiler.TreeNodes.Types
{
    public class VarTypeNode : TypeNode
    {
        VarTypeNode(){}
        public VarTypeNode(Token token)
        {
            this.token = token;
        }
    }
}