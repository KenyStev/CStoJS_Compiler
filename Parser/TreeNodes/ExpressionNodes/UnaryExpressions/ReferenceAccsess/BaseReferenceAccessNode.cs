using System;
using Compiler.SemanticAPI;
using Compiler.SemanticAPI.ContextUtils;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions.ReferenceAccsess
{
    public class BaseReferenceAccessNode : ReferenceAccsessNode
    {
        public BaseReferenceAccessNode(){}
        public BaseReferenceAccessNode(Token token) : base(token) {}

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            return api.contextManager.getTypeFromContext(ContextType.BASE);
        }
    }
}