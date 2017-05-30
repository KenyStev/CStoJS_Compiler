using System.Xml.Serialization;
using Compiler.TreeNodes.Expressions.UnaryExpressions;

namespace Compiler.TreeNodes.Types
{
    public class AbstractTypeNode : TypeNode
    {
        [XmlElement(typeof(IdNode))]
        public IdNode Identifier;

        private AbstractTypeNode(){}
        public AbstractTypeNode(IdNode typeName,Token token)
        {
            this.Identifier = typeName;
            this.token = token;
        }
    }
}