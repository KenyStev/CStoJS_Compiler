using System.Xml.Serialization;
using Compiler.TreeNodes.Expressions.UnaryExpressions;

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