using Compiler.TreeNodes.Expressions.UnaryExpressions;

namespace Compiler.TreeNodes.Expressions.MultipicativeExpressions
{
    public class MultipicativeExpressionNode : BinaryOperatorNode
    {
        public MultipicativeExpressionNode(){}
        public MultipicativeExpressionNode(ExpressionNode leftExpression, 
        UnaryExpressionNode unaryExpression,Token token) : base(leftExpression,unaryExpression,token)
        {
        }
    }
}