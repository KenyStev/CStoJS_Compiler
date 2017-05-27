using System.Xml.Serialization;

namespace Compiler.TreeNodes.Types
{
    public class PrimitiveTypeNode : TypeNode
    {
        private TokenType DataType;

        private PrimitiveTypeNode()
        {
            this.DataType = TokenType.EOF;
        }
        public PrimitiveTypeNode(TokenType type)
        {
            this.DataType = type;
        }
    }
}