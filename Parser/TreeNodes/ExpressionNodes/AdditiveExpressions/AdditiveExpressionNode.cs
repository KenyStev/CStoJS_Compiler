using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.AdditiveExpressions
{
    public abstract class AdditiveExpressionNode : BinaryOperatorNode
    {
        public AdditiveExpressionNode(){}
        public AdditiveExpressionNode(ExpressionNode leftExpression, 
        ExpressionNode multExpression,Token token) : base(leftExpression,multExpression,token)
        {
            var tInt = new IntTypeNode();
            var tFloat = new FloatTypeNode();
            rules[Utils.Int + "," + Utils.Int] = tInt;
            rules[Utils.Int + "," + Utils.Char] = tInt;
            rules[Utils.Char + "," + Utils.Int] = tInt;
            rules[Utils.Char + "," + Utils.Char] = tInt;

            rules[Utils.Float + "," + Utils.Int] = tFloat;
            rules[Utils.Int + "," + Utils.Float] = tFloat;
            rules[Utils.Float + "," + Utils.Float] = tFloat;
            rules[Utils.Float + "," + Utils.Char] = tFloat;
            rules[Utils.Char + "," + Utils.Float] = tFloat;
        }
    }
}