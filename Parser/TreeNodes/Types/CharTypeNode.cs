using System;
using System.Xml.Serialization;
using Compiler.SemanticAPI;

namespace Compiler.TreeNodes.Types
{
    public class CharTypeNode : PrimitiveTypeNode
    {
        public CharTypeNode(){}
        public CharTypeNode(Token type) : base(type){}

        public override void Evaluate(API api)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "CharType";
        }

        public override bool Equals(object obj)
        {
            return obj is CharTypeNode;
        }

        public override string getComparativeType()
        {
            return Utils.Char;
        }
    }
}