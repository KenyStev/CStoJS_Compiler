using System;
using System.Xml.Serialization;
using Compiler.SemanticAPI;

namespace Compiler.TreeNodes.Types
{
    public class IntTypeNode : PrimitiveTypeNode
    {
        public IntTypeNode(){}
        public IntTypeNode(Token type) : base(type){}

        public override void Evaluate(API api)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "IntType";
        }

        public override bool Equals(object obj)
        {
            return obj is IntTypeNode;
        }

        public override string getComparativeType()
        {
            return Utils.Int;
        }
    }
}