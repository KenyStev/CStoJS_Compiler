namespace Compiler.TreeNodes.Types
{
    internal class PrimitiveTypeNode : TypeNode
    {
        private TokenType type;

        public PrimitiveTypeNode(TokenType type)
        {
            this.type = type;
        }
    }
}