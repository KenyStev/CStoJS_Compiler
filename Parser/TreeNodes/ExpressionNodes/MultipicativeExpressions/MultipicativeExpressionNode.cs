using Compiler.TreeNodes.Expressions.UnaryExpressions;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.MultipicativeExpressions
{
    public class MultipicativeExpressionNode : BinaryOperatorNode
    {
        public MultipicativeExpressionNode(){}
        public MultipicativeExpressionNode(ExpressionNode leftExpression, 
        UnaryExpressionNode unaryExpression,Token token) : base(leftExpression,unaryExpression,token)
        {
            var tInt = new IntTypeNode();
            var tFloat = new FloatTypeNode();
            rules[Utils.Int + "," + Utils.Int] = tInt;
            rules[Utils.Char + "," + Utils.Int] = tInt;
            rules[Utils.Int + "," + Utils.Char] = tInt;
            rules[Utils.Int + "," + Utils.Float] = tFloat;
            rules[Utils.Float + "," + Utils.Int] = tFloat;

            rules[Utils.Float + "," + Utils.Float] = tFloat;
            rules[Utils.Float + "," + Utils.Char] = tFloat;
            rules[Utils.Char + "," + Utils.Float] = tFloat;

            rules[Utils.Char + "," + Utils.Char] = tInt;
        }
    }
}