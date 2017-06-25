using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions
{
    public class ExclusiveOrExpression : BinaryOperatorNode
    {
        private ExclusiveOrExpression(){}
        public ExclusiveOrExpression(ExpressionNode leftExpression, ExpressionNode bitsAnd,Token token) : 
        base(leftExpression,bitsAnd,token)
        {
            rules[Utils.Bool + "," + Utils.Bool] = new BoolTypeNode();
            var t = new IntTypeNode();
            rules[Utils.Char + "," + Utils.Char] = t;
            rules[Utils.Char + "," + Utils.Int] = t;
            rules[Utils.Int + "," + Utils.Char] = t;
            rules[Utils.Int + "," + Utils.Int] = t;
        }
    }
}