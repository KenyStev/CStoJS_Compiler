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
            return new NullTypeNode();
        }

        public override void GenerateCode(Writer.Writer Writer, API api)
        {
            Writer.WriteString("null");
        }
    }
}