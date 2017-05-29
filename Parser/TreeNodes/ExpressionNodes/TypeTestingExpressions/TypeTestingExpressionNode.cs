using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.TypeTestingExpressions
{
    public class TypeTestingExpressionNode : ExpressionNode
    {
        public ExpressionNode leftExpression;
        public TypeNode type;

        public TypeTestingExpressionNode(){}
        public TypeTestingExpressionNode(ExpressionNode leftExpression, TypeNode type)
        {
            this.leftExpression = leftExpression;
            this.type = type;
        }
    }
}