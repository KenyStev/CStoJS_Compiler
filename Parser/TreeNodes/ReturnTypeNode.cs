using System.Xml.Serialization;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes
{
    public class ReturnTypeNode
    {
        [XmlElement(typeof(TypeNode)),
        XmlElement(typeof(PrimitiveTypeNode),ElementName = "PrimitiveType"),
        XmlElement(typeof(AbstractTypeNode),ElementName = "AbstractType"),
        XmlElement(typeof(ArrayTypeNode),ElementName = "ArrayType"),
        XmlElement(typeof(VoidTypeNode),ElementName = "VoidType")]
        public TypeNode DataType;

        [XmlElement(typeof(bool))]
        public bool IsVoid;

        private ReturnTypeNode()
        {
            DataType = null;
            IsVoid = false;
        }
        public ReturnTypeNode(TypeNode typeNode, bool isVoid)
        {
            this.DataType = typeNode;
            this.IsVoid = isVoid;
        }
    }
}