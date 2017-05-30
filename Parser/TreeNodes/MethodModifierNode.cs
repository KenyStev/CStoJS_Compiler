using System.Xml.Serialization;

namespace Compiler.TreeNodes
{
    public class MethodModifierNode
    {
        public Token token;

        private MethodModifierNode(){}
        public MethodModifierNode(Token optionalModifierToken)
        {
            this.token = optionalModifierToken;
        }
    }
}