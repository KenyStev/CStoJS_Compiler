using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.TypeTestingExpressions
{
    public class AsTypeTestNode : TypeTestingExpressionNode
    {
        AsTypeTestNode(){}
        public AsTypeTestNode(ExpressionNode leftExpression, TypeNode type) : base(leftExpression, type)
        {
        }
    }
}