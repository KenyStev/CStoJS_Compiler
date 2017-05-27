using System.Collections.Generic;

namespace Compiler.TreeNodes.Types
{
    public class ArrayTypeNode : TypeNode
    {
        private TypeNode type;
        private List<MultidimensionArrayTypeNode> multidimsArrays;

        public ArrayTypeNode(TypeNode type, List<MultidimensionArrayTypeNode> multidimsArrays)
        {
            this.type = type;
            this.multidimsArrays = multidimsArrays;
        }
    }
}