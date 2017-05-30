using System.Xml.Serialization;

namespace Compiler.TreeNodes.Types
{
    public class PrimitiveTypeNode : TypeNode
    {
        private PrimitiveTypeNode(){}
        public PrimitiveTypeNode(Token type)
        {
            this.token = type;
        }
    }
}