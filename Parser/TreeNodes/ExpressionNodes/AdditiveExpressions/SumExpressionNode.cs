using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.AdditiveExpressions
{
    public class SumExpressionNode : AdditiveExpressionNode
    {
        SumExpressionNode(){}
        public SumExpressionNode(ExpressionNode leftExpression, 
        ExpressionNode multExpression,Token token) : base(leftExpression,multExpression,token)
        {
            var s = new StringTypeNode();
            rules[Utils.Char + "," + Utils.String] = s;
            rules[Utils.String + "," + Utils.Char] = s;
            rules[Utils.String + "," + Utils.String] = s;
            rules[Utils.String + "," + Utils.Int] = s;
            rules[Utils.Int + "," + Utils.String] = s;
            rules[Utils.String + "," + Utils.Float] = s;
            rules[Utils.Float + "," + Utils.String] = s;
        }
    }
}