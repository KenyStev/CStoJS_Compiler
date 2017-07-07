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
        public Token token;

        private ReturnTypeNode(){}
        public ReturnTypeNode(TypeNode typeNode, bool isVoid,Token token)
        {
            this.DataType = typeNode;
            this.IsVoid = isVoid;
            this.token = token;
        }

        public override bool Equals(object obj)
        {
            if (obj is ReturnTypeNode)
            {
                var o = obj as ReturnTypeNode;
                return DataType.Equals(o.DataType);
            }
            return false;
        }
    }
}