using System.Xml.Serialization;
using Compiler.TreeNodes.Types;
using Compiler.TreeNodes.Expressions.UnaryExpressions;

namespace Compiler.TreeNodes
{
    public class ParameterNode
    {
        [XmlElement(typeof(TypeNode)),
        XmlElement(typeof(PrimitiveTypeNode)),
        XmlElement(typeof(AbstractTypeNode)),
        XmlElement(typeof(ArrayTypeNode))]
        public TypeNode DataType;

        [XmlElement(typeof(IdNode))]
        public IdNode paramName;
        public Token token;

        private ParameterNode(){}
        public ParameterNode(TypeNode type, IdNode paramName,Token token)
        {
            this.DataType = type;
            this.paramName = paramName;
            this.token = token;
        }
    }
}