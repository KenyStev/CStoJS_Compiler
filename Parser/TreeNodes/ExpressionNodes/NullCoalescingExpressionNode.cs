using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions
{
    public class NullCoalescingExpressionNode : ExpressionNode
    {
        public ExpressionNode nullableExpression;
        public ExpressionNode rightExpression;

        private NullCoalescingExpressionNode(){}
        public NullCoalescingExpressionNode(ExpressionNode nullableExpression, 
        ExpressionNode rightExpression,Token token) : base(token)
        {
            this.nullableExpression = nullableExpression;
            this.rightExpression = rightExpression;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            var tnull = nullableExpression.EvaluateType(api,null,true);
            var rexp = rightExpression.EvaluateType(api,null,true);
            if(tnull is PrimitiveTypeNode || rexp is PrimitiveTypeNode)
                Utils.ThrowError("Operator '??' cannot be applied to operands of type '"+
                tnull.ToString()+"' and '"+rexp.ToString()+"' ["+
                api.currentNamespace.Identifier.Name+"]");
            if(tnull is NullTypeNode)
                return rexp;
            return tnull;
        }
    }
}