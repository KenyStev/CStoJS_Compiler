using System.Xml.Serialization;
using Compiler.TreeNodes.Types;

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

        private ParameterNode()
        {
            this.DataType = null;
            paramName = null;
        }
        public ParameterNode(TypeNode type, IdNode paramName)
        {
            this.DataType = type;
            this.paramName = paramName;
        }
    }
}