using System.Xml.Serialization;

namespace Compiler.TreeNodes
{
    public class MethodModifierNode
    {
        [XmlAttribute(AttributeName = "type")]
        public TokenType type;

        private MethodModifierNode()
        {
            type = TokenType.EOF;
        }
        public MethodModifierNode(TokenType type)
        {
            this.type = type;
        }
    }
}