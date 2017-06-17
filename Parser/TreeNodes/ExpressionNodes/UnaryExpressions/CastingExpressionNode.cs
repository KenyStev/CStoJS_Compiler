using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class CastingExpressionNode : UnaryExpressionNode
    {
        public TypeNode targetCastType;
        public UnaryExpressionNode expresion;

        public CastingExpressionNode(){}

        public CastingExpressionNode(TypeNode targetCastType, UnaryExpressionNode exp,Token token) : base(token)
        {
            this.targetCastType = targetCastType;
            this.expresion = exp;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            throw new NotImplementedException();
        }
    }
}