using System.Collections.Generic;
using System.Xml.Serialization;

namespace Compiler.TreeNodes
{
    public class ConstructorInitializerNode
    {
        [XmlArray("Arguments"),
        XmlArrayItem("Argument")]
        public List<ArgumentNode> arguments;
        public Token token;

        private ConstructorInitializerNode(){}
        public ConstructorInitializerNode(List<ArgumentNode> arguments,Token token)
        {
            this.arguments = arguments;
            this.token = token;
        }
    }
}