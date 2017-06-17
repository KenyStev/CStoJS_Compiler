using System;
using System.Collections.Generic;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions.InstanceExpressions
{
    public class ClassInstantioationNode : InstanceExpressionNode
    {
        public TypeNode type;
        public List<ArgumentNode> arguments;

        public ClassInstantioationNode(){}

        public ClassInstantioationNode(TypeNode type, List<ArgumentNode> arguments,Token token) : base(token)
        {
            this.type = type;
            this.arguments = arguments;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            throw new NotImplementedException();
        }
    }
}