using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class AccessorNode : PrimaryExpressionNode
    {
        public PrimaryExpressionNode primary;
        public IdNode identifier;

        public AccessorNode(){}

        public AccessorNode(PrimaryExpressionNode primary, IdNode identifier,Token token) : base(token)
        {
            this.primary = primary;
            this.identifier = identifier;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            throw new NotImplementedException();
        }
    }
}