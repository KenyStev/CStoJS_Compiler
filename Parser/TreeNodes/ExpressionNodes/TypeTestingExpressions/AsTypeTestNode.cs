using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.TypeTestingExpressions
{
    public class AsTypeTestNode : TypeTestingExpressionNode
    {
        AsTypeTestNode(){}
        public AsTypeTestNode(ExpressionNode leftExpression, TypeNode type,Token token) : base(leftExpression, type,token)
        {
        }
    }
}