using System;
using System.Xml.Serialization;
using Compiler.SemanticAPI;

namespace Compiler.TreeNodes.Types
{
    public class PrimitiveTypeNode : TypeNode
    {
        private PrimitiveTypeNode(){}
        public PrimitiveTypeNode(Token type)
        {
            this.token = type;
        }

        public override void Evaluate(API api)//TODO
        {
            throw new NotImplementedException();
        }
    }
}