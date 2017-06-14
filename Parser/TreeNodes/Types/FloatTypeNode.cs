using System;
using System.Xml.Serialization;
using Compiler.SemanticAPI;

namespace Compiler.TreeNodes.Types
{
    public class FloatTypeNode : PrimitiveTypeNode
    {
        public FloatTypeNode(){}
        public FloatTypeNode(Token type) : base(type){}

        public override void Evaluate(API api) //TODO
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "FloatType";
        }
    }
}