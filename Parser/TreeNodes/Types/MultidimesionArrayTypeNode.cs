using System.Xml.Serialization;

namespace Compiler.TreeNodes.Types
{
    public class MultidimensionArrayTypeNode
    {
        [XmlAttribute(AttributeName = "dimensions")]
        public int dimensions;
        public Token token;

        private MultidimensionArrayTypeNode(){}
        public MultidimensionArrayTypeNode(int dimensions,Token token)
        {
            this.dimensions = dimensions;
            this.token = token;
        }

        public override string ToString()
        {
            string rank = "[";
            for (int i = 1; i < dimensions; i++)
            {
                rank += ",";
            }
            return rank + "]";
        }

        public override bool Equals(object obj)
        {
            if(obj is MultidimensionArrayTypeNode)
            {
                var o =  obj as MultidimensionArrayTypeNode;
                return dimensions == o.dimensions;
            }
            return false;
        }
    }
}