using System;
using Compiler.SemanticAPI;
using Compiler.SemanticAPI.ContextUtils;
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
            if(api.contextManager.currentContext.contextName=="if:line(58,17)" && this.primary.ToString() == "Name")
                Console.Write("");
            var isStaticNew = isStatic;
            // if(!api.isNextStaticContext)
            //     isStaticNew = false;
            TypeNode t = primary.EvaluateType(api,type,isStatic);
            // if(primary is BaseReferenceAccessNode || primary is ThisReferenceAccsessNode)
            // if(!api.isNextStaticContext)
            //     isStaticNew = false;
            if(nextExpression!=null)
                t = nextExpression.EvaluateType(api,t,api.isNextStaticContext);
            return t;
        }
    }
}