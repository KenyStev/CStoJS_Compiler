using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions
{
    public class TernaryExpressionNode : ExpressionNode
    {
        public ExpressionNode conditionalExpression;
        public ExpressionNode trueExpression;
        public ExpressionNode falseExpression;

        private TernaryExpressionNode(){}
        public TernaryExpressionNode(ExpressionNode conditionalExpression, ExpressionNode trueExpression, 
        ExpressionNode falseExpression,Token token) : base(token)
        {
            this.conditionalExpression = conditionalExpression;
            this.trueExpression = trueExpression;
            this.falseExpression = falseExpression;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            throw new NotImplementedException();
        }
    }
}