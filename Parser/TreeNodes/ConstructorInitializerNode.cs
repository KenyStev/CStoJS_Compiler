using System.Collections.Generic;
using System.Xml.Serialization;

namespace Compiler.TreeNodes
{
    public class ConstructorInitializerNode
    {
        [XmlArray("Arguments"),
        XmlArrayItem("Argument")]
        public List<ArgumentNode> arguments;

        private ConstructorInitializerNode()
        {
            arguments = null;
        }
        public ConstructorInitializerNode(List<ArgumentNode> arguments)
        {
            this.arguments = arguments;
        }
    }
}