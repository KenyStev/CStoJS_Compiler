using System.Xml.Serialization;

namespace Compiler.TreeNodes
{
    public class EncapsulationNode
    {
        [XmlAttribute(AttributeName = "type")]
        public TokenType type;

        private EncapsulationNode()
        {
            this.type = TokenType.EOF;
        }
        public EncapsulationNode(TokenType type)
        {
            this.type = type;
        }
    }
}