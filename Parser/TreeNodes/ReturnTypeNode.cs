using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes
{
    public class ReturnTypeNode
    {
        private TypeNode typeNode;
        private bool isVoid;

        public ReturnTypeNode(TypeNode typeNode, bool isVoid)
        {
            this.typeNode = typeNode;
            this.isVoid = isVoid;
        }

        public bool IsVoid()
        {
            return isVoid;
        }
    }
}