using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions
{
    public class ConditionalAndExpressionNode : BinaryOperatorNode
    {
        private ConditionalAndExpressionNode(){}
        public ConditionalAndExpressionNode(ExpressionNode leftExpression, ExpressionNode bitsOt,
        Token token) : base(leftExpression,bitsOt,token)
        {
            rules[Utils.Bool + "," + Utils.Bool] = new BoolTypeNode();
        }
    }
}