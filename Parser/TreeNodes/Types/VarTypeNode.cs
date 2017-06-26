using System;
using Compiler.SemanticAPI;

namespace Compiler.TreeNodes.Types
{
    public class VarTypeNode : TypeNode
    {
        VarTypeNode(){}
        public VarTypeNode(Token token)
        {
            this.token = token;
        }

        public override void Evaluate(API api)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "VarType";
        }

        public override bool Equals(object obj)
        {
            return obj is VarTypeNode;
        }

        public override string getComparativeType()
        {
            return "VarType";
        }
    }
}