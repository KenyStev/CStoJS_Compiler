using System;
using Compiler.SemanticAPI;

namespace Compiler.TreeNodes.Types
{
    public class NullTypeNode : TypeNode
    {
        NullTypeNode(){}
        public NullTypeNode(Token token)
        {
            this.token = token;
        }

        public override void Evaluate(API api)//TODO
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "NullType";
        }

        public override bool Equals(object obj)
        {
            return obj is NullTypeNode;
        }

        public override string getComparativeType()
        {
            return Utils.Null;
        }
    }
}