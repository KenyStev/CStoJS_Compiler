using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class InlineExpressionNode : UnaryExpressionNode
    {
        public UnaryExpressionNode primary;
        public InlineExpressionNode nextExpression;
        
        public InlineExpressionNode(){}

        public InlineExpressionNode(UnaryExpressionNode primary, Token token)
        {
            this.primary = primary;
            this.nextExpression = null;
            this.token = token;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            throw new NotImplementedException();
        }
    }
}