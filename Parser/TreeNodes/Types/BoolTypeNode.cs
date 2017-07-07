using System;
using System.Xml.Serialization;
using Compiler.SemanticAPI;

namespace Compiler.TreeNodes.Types
{
    public class BoolTypeNode : PrimitiveTypeNode
    {
        public BoolTypeNode(){}
        public BoolTypeNode(Token type) : base(type){}

        public override void Evaluate(API api)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "BoolType";
        }

        public override bool Equals(object obj)
        {
            return obj is BoolTypeNode;
        }

        public override string getComparativeType()
        {
            return Utils.Bool;
        }
    }
}