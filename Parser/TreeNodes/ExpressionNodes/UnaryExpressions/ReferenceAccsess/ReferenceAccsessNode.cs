using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions.ReferenceAccsess
{
    public class ReferenceAccsessNode : PrimaryExpressionNode
    {

        public ReferenceAccsessNode(){}
        public ReferenceAccsessNode(Token token)
        {
            this.token = token;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            throw new NotImplementedException();
        }
    }
}