using System.Xml.Serialization;

namespace Compiler.TreeNodes.Types
{
    public class AbstractTypeNode : TypeNode
    {
        [XmlElement(typeof(IdNode))]
        public IdNode Identifier;

        private AbstractTypeNode()
        {
            Identifier = null;
        }
        public AbstractTypeNode(IdNode typeName)
        {
            this.Identifier = typeName;
        }
    }
}