using System.Xml.Serialization;

namespace Compiler.TreeNodes
{
    public class EncapsulationNode
    {
        [XmlAttribute(AttributeName = "type")]
        public TokenType type;
        public Token token;

        private EncapsulationNode(){}
        public EncapsulationNode(TokenType type,Token token)
        {
            this.type = type;
            this.token = token;
        }
    }
}