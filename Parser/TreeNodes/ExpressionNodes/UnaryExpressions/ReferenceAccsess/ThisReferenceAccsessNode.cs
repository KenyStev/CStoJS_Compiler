using System;
using Compiler.SemanticAPI;
using Compiler.SemanticAPI.ContextUtils;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions.ReferenceAccsess
{
    public class ThisReferenceAccsessNode : ReferenceAccsessNode
    {
        public ThisReferenceAccsessNode(){}
        public ThisReferenceAccsessNode(Token token) : base(token) {}

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            api.isNextStaticContext = false;
            return api.contextManager.getTypeFromContext(ContextType.CLASS);
        }
    }
}