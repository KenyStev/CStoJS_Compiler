using System;
using System.Xml.Serialization;
using Compiler.SemanticAPI;

namespace Compiler.TreeNodes.Types
{
    public abstract class PrimitiveTypeNode : TypeNode
    {
        public PrimitiveTypeNode(){}
        public PrimitiveTypeNode(Token type)
        {
            this.token = type;
        }
    }
}