using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions
{
    public abstract class BinaryOperatorNode : ExpressionNode
    {
        public ExpressionNode leftOperand;
        public ExpressionNode rightOperand;

        public BinaryOperatorNode(){} 
        public BinaryOperatorNode(ExpressionNode leftExpression, ExpressionNode relationalExpression,
        Token token) : base(token)
        {
            this.leftOperand = leftExpression;
            this.rightOperand = relationalExpression;
            this.token = token;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            throw new NotImplementedException();
        }
    }
}