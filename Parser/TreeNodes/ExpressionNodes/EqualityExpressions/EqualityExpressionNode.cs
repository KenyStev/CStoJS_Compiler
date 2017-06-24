using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.EqualityExpressions
{
    public class EqualityExpressionNode : BinaryOperatorNode
    {
        public EqualityExpressionNode(){}
        public EqualityExpressionNode(ExpressionNode leftExpression, 
        ExpressionNode relationalExpression,Token token) : base(leftExpression,relationalExpression,token)
        {
            var t=new BoolTypeNode();
            rules[Utils.Char + "," + Utils.Char] = t;
            rules[Utils.Char + "," + Utils.Int] = t;
            rules[Utils.Int + "," + Utils.Char] = t;
            rules[Utils.Char + "," + Utils.Float] = t;
            rules[Utils.Float + "," + Utils.Char] = t;

            rules[Utils.Int + "," + Utils.Int] = t;
            rules[Utils.Int + "," + Utils.Float] = t;
            rules[Utils.Float + "," + Utils.Int] = t;

            rules[Utils.Float + "," + Utils.Float] = t;

            rules[Utils.Bool + "," + Utils.Bool] = t;
            rules[Utils.String + "," + Utils.String] = t;
            rules[Utils.Class + "," + Utils.Class] = t;
            rules[Utils.Enum + "," + Utils.Enum] = t;
            rules[Utils.Class + "," + Utils.Null] = t;
            rules[Utils.Null + "," + Utils.Class] = t;
            rules[Utils.Array + "," + Utils.Array] = t;
        }
    }
}