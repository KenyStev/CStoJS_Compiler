using System;
using System.Xml.Serialization;
using Compiler.SemanticAPI;

namespace Compiler.TreeNodes.Types
{
    public class StringTypeNode : PrimitiveTypeNode
    {
        public StringTypeNode(){}
        public StringTypeNode(Token type) : base(type){}

        public override void Evaluate(API api) //TODO
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "StringType";
        }

        public override bool Equals(object obj)
        {
            return obj is StringTypeNode;
        }

        public override string getComparativeType()
        {
            return Utils.String;
        }
    }
}