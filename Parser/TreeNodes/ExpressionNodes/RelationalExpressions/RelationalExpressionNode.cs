using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.RelationalExpressions
{
    public class RelationalExpressionNode : BinaryOperatorNode
    {
        public RelationalExpressionNode(){}

        public RelationalExpressionNode(ExpressionNode leftExpression, 
        ExpressionNode shiftExpression,Token token) : base(leftExpression,shiftExpression,token)
        {
            var t = new BoolTypeNode();
            rules[Utils.Char + "," + Utils.Char] = t;
            rules[Utils.Char + "," + Utils.Int] = t;
            rules[Utils.Int + "," + Utils.Char] = t;
            rules[Utils.Char + "," + Utils.Float] = t;
            rules[Utils.Float + "," + Utils.Char] = t;

            rules[Utils.Int + "," + Utils.Int] = t;
            rules[Utils.Int + "," + Utils.Float] = t;
            rules[Utils.Float + "," + Utils.Int] = t;

            rules[Utils.Float + "," + Utils.Float] = t;
        }
        
    }
}