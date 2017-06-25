using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Expressions.UnaryExpressions.ReferenceAccsess;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class InlineExpressionNode : UnaryExpressionNode
    {
        public PrimaryExpressionNode primary;
        public InlineExpressionNode nextExpression;
        
        public InlineExpressionNode(){}

        public InlineExpressionNode(PrimaryExpressionNode primary, Token token)
        {
            this.primary = primary;
            this.nextExpression = null;
            this.token = token;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            var isStaticNew = isStatic;
            TypeNode t = primary.EvaluateType(api,type,isStatic);
            if(primary is BaseReferenceAccessNode || primary is ThisReferenceAccsessNode)
                isStaticNew = false;
            if(nextExpression!=null)
                t = nextExpression.EvaluateType(api,t,isStaticNew);
            return t;
        }
    }
}