using System;
using System.Xml.Serialization;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions
{
    public class AssignExpressionNode : ExpressionNode
    {
        public ExpressionNode unaryExpression;
        public TokenType assignType;
        public ExpressionNode assignExpression;

        public AssignExpressionNode(){}
        public AssignExpressionNode(ExpressionNode unaryExpression, TokenType assignType, 
        ExpressionNode assignExpression,Token token) : base(token)
        {
            this.unaryExpression = unaryExpression;
            this.assignType = assignType;
            this.assignExpression = assignExpression;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            throw new NotImplementedException();
        }
    }
}