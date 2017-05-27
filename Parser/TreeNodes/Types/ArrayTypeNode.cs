using System.Collections.Generic;
using System.Xml.Serialization;

namespace Compiler.TreeNodes.Types
{
    public class ArrayTypeNode : TypeNode
    {
        [XmlElement(typeof(TypeNode))]
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