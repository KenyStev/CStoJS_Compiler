using System.Xml.Serialization;

namespace Compiler.TreeNodes
{
    public class EncapsulationNode
    {
        [XmlAttribute(AttributeName = "type")]
        public TokenType type;
        public Token token;

        private EncapsulationNode()
        {
            this.type = TokenType.EOF;
        }
        public EncapsulationNode(TokenType type,Token token)
        {
            this.type = type;
            this.token = token;
        }
    }
}