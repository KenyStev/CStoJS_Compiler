using System;
using System.Collections.Generic;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class FunctionCallExpressionNode : PrimaryExpressionNode
    {
        public PrimaryExpressionNode identifier;
        public List<ArgumentNode> arguments;

        public FunctionCallExpressionNode(){}

        public FunctionCallExpressionNode(List<ArgumentNode> arguments,Token token) : base(token)
        {
            this.identifier = null;
            this.arguments = arguments;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            throw new NotImplementedException();
        }
    }
}