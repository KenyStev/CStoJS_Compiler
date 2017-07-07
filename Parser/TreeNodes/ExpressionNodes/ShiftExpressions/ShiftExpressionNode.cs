using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.ShiftExpressions
{
    public class ShiftExpressionNode : BinaryOperatorNode
    {
        public ShiftExpressionNode(){}
        public ShiftExpressionNode(ExpressionNode leftExpression, 
        ExpressionNode additiveExpression,Token token) : base(leftExpression,additiveExpression,token)
        {
            var t = new IntTypeNode();
            rules[Utils.Char + "," + Utils.Char] = t;
            rules[Utils.Char + "," + Utils.Int] = t;
            rules[Utils.Int + "," + Utils.Char] = t;
            rules[Utils.Int + "," + Utils.Int] = t;
        }
    }
}