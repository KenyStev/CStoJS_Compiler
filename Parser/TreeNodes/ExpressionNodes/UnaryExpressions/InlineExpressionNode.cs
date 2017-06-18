using System;
using Compiler.SemanticAPI;
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
            TypeNode t = primary.EvaluateType(api,type,isStatic);
            if(nextExpression!=null)
                t = nextExpression.EvaluateType(api,t,isStatic);
            return t;
        }
    }
}