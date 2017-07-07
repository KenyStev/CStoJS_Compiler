using Compiler.TreeNodes.Expressions.UnaryExpressions;

namespace Compiler.TreeNodes.Expressions.MultipicativeExpressions
{
    public class MultNode : MultipicativeExpressionNode
    {

        MultNode(){}
        public MultNode(ExpressionNode leftExpression, 
        UnaryExpressionNode unaryExpression,Token token) : base(leftExpression,unaryExpression,token)
        {
            
        }
    }
}