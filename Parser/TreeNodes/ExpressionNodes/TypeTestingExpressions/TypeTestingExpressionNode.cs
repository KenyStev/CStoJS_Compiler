using System.Collections.Generic;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.TypeTestingExpressions
{
    public abstract class TypeTestingExpressionNode : ExpressionNode
    {
        public ExpressionNode leftExpression;
        public TypeNode toType;
        public List<string> rules;

        public TypeTestingExpressionNode(){}
        public TypeTestingExpressionNode(ExpressionNode leftExpression, TypeNode type,Token token)
        {
            rules = new List<string>();
            this.leftExpression = leftExpression;
            this.toType = type;
            this.token = token;
        }
    }
}