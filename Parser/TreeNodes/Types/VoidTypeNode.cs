using System;
using Compiler.SemanticAPI;

namespace Compiler.TreeNodes.Types
{
    public class VoidTypeNode : TypeNode
    {
        VoidTypeNode(){}
        public VoidTypeNode(Token token)
        {
            this.token = token;
        }

        public override void Evaluate(API api)//TODO
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "VoidType";
        }
    }
}