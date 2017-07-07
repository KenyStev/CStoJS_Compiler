using System;
using System.Xml.Serialization;
using Compiler.SemanticAPI;

namespace Compiler.TreeNodes.Types
{
    public class FloatTypeNode : PrimitiveTypeNode
    {
        public FloatTypeNode(){}
        public FloatTypeNode(Token type) : base(type){}

        public override void Evaluate(API api)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "FloatType";
        }

        public override bool Equals(object obj)
        {
            return obj is FloatTypeNode;
        }

        public override string getComparativeType()
        {
            return Utils.Float;
        }
    }
}