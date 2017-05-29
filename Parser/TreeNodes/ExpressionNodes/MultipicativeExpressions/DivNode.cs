using Compiler.TreeNodes.Expressions.UnaryExpressions;

namespace Compiler.TreeNodes.Expressions.MultipicativeExpressions
{
    public class DivNode : MultipicativeExpressionNode
    {
        DivNode(){}
        public DivNode(ExpressionNode leftExpression, UnaryExpressionNode unaryExpression) : base(leftExpression, unaryExpression)
        {
        }
    }
}