using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class GroupedExpressionNode : PrimaryExpressionNode
    {
        public ExpressionNode expression;

        public GroupedExpressionNode(){}

        public GroupedExpressionNode(ExpressionNode exp,Token token) : base(token)
        {
            this.expression = exp;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            throw new NotImplementedException();
        }
    }
}