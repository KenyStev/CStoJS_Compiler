using Compiler.TreeNodes.Expressions.UnaryExpressions;

namespace Compiler.TreeNodes.Expressions.MultipicativeExpressions
{
    public class ModNode : MultipicativeExpressionNode
    {
        ModNode(){}
        public ModNode(ExpressionNode leftExpression, 
        UnaryExpressionNode unaryExpression,Token token) : base(leftExpression, unaryExpression,token)
        {
        }
    }
}