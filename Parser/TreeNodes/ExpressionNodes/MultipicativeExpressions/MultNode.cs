using Compiler.TreeNodes.Expressions.UnaryExpressions;

namespace Compiler.TreeNodes.Expressions.MultipicativeExpressions
{
    public class MultNode : MultipicativeExpressionNode
    {

        MultNode(){}
        public MultNode(ExpressionNode leftExpression, UnaryExpressionNode unaryExpression) : base(leftExpression,unaryExpression)
        {
            
        }
    }
}