using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions.InstanceExpressions
{
    public class NullExpressionNode : PrimaryExpressionNode
    {
        public NullExpressionNode(){}
        public NullExpressionNode(Token token)
        {
            this.token = token;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            throw new NotImplementedException();
        }
    }
}