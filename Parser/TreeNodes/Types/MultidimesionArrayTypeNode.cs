using System.Xml.Serialization;

namespace Compiler.TreeNodes.Types
{
    public class MultidimensionArrayTypeNode
    {
        [XmlAttribute(AttributeName = "dimensions")]
        public int dimensions;

        private MultidimensionArrayTypeNode(){}
        public MultidimensionArrayTypeNode(int dimensions)
        {
            this.dimensions = dimensions;
        }
    }
}