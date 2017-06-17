using System;
using System.Collections.Generic;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class ArrayAccessExpressionNode : PrimaryExpressionNode
    {
        public PrimaryExpressionNode identifier;
        public List<List<ExpressionNode>> arrayAccessList;

        public ArrayAccessExpressionNode(){}

        public ArrayAccessExpressionNode(List<List<ExpressionNode>> arrayAccessList,Token token) : base(token)
        {
            this.identifier = null;
            this.arrayAccessList = arrayAccessList;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            throw new NotImplementedException();
        }
    }
}