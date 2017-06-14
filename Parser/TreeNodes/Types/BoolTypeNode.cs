using System;
using System.Xml.Serialization;
using Compiler.SemanticAPI;

namespace Compiler.TreeNodes.Types
{
    public class BoolTypeNode : PrimitiveTypeNode
    {
        public BoolTypeNode(){}
        public BoolTypeNode(Token type) : base(type){}

        public override void Evaluate(API api) //TODO
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "BoolType";
        }
    }
}