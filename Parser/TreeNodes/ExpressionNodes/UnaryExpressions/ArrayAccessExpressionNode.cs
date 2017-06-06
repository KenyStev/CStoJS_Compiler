using System.Collections.Generic;

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
    }
}