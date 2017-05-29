using System.Collections.Generic;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class ArrayAccessExpressionNode : PrimaryExpressionNode
    {
        public PrimaryExpressionNode identifier;
        public List<List<ExpressionNode>> arrayAccessList;

        public ArrayAccessExpressionNode(){}

        public ArrayAccessExpressionNode(PrimaryExpressionNode identifier, List<List<ExpressionNode>> arrayAccessList)
        {
            this.identifier = identifier;
            this.arrayAccessList = arrayAccessList;
        }
    }
}