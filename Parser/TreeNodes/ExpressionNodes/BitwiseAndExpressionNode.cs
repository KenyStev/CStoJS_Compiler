using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions
{
    public class BitwiseAndExpressionNode : BinaryOperatorNode
    {
        BitwiseAndExpressionNode(){}
        public BitwiseAndExpressionNode(ExpressionNode leftExpression, ExpressionNode equalityExpression,
        Token token) : base(leftExpression,equalityExpression,token)
        {
            var t = new IntTypeNode();
            rules[Utils.Char + "," + Utils.Char] = t;
            rules[Utils.Char + "," + Utils.Int] = t;
            rules[Utils.Int + "," + Utils.Char] = t;
            rules[Utils.Int + "," + Utils.Int] = t;
        }
    }
}