using System.Collections.Generic;
using System.Xml.Serialization;

namespace Compiler.TreeNodes.Types
{
    [XmlType("ArrayType")]
    public class ArrayTypeNode : TypeNode
    {
        [XmlElement(typeof(TypeNode)),
        XmlElement(typeof(PrimitiveTypeNode),ElementName = "PrimitiveTypeNode"),
        XmlElement(typeof(AbstractTypeNode),ElementName = "AbstractTypeNode")]
        public TypeNode DataType;

        [XmlArray("MultiDimArrays"),
        XmlArrayItem("MultiDimArray")]
        public List<MultidimensionArrayTypeNode> multidimsArrays;

        private ArrayTypeNode()
        {
            this.DataType = null;
            multidimsArrays = null;
        }
        public ArrayTypeNode(TypeNode type, List<MultidimensionArrayTypeNode> multidimsArrays)
        {
            this.DataType = type;
            this.multidimsArrays = multidimsArrays;
        }
    }
}