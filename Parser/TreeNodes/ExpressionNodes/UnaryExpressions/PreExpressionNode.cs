using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class PreExpressionNode : UnaryExpressionNode
    {
        public TokenType unaryOperator;
        public UnaryExpressionNode unaryExpression;

        public PreExpressionNode(){}

        public PreExpressionNode(TokenType unaryOperator, UnaryExpressionNode unaryExpression,Token token) : base(token)
        {
            this.unaryOperator = unaryOperator;
            this.unaryExpression = unaryExpression;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            throw new NotImplementedException();
        }
    }
}