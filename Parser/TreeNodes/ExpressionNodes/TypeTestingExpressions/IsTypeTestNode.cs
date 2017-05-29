using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.TypeTestingExpressions
{
    public class IsTypeTestNode : TypeTestingExpressionNode
    {
        IsTypeTestNode(){}
        public IsTypeTestNode(ExpressionNode leftExpression, TypeNode type) : base(leftExpression,type)
        {
            
        }
    }
}