using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions
{
    public class BitwiseOrExpressionNode : BinaryOperatorNode
    {
        private BitwiseOrExpressionNode(){}
        public BitwiseOrExpressionNode(ExpressionNode leftExpression, ExpressionNode exclusiveOrExpression,
        Token token) :  base(leftExpression,exclusiveOrExpression,token)
        {
            var t = new IntTypeNode();
            rules[Utils.Char + "," + Utils.Char] = t;
            rules[Utils.Char + "," + Utils.Int] = t;
            rules[Utils.Int + "," + Utils.Char] = t;
            rules[Utils.Int + "," + Utils.Int] = t;
        }
    }
}