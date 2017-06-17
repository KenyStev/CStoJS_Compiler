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
            throw new NotImplementedException();
        }
    }
}