using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class CastingExpressionNode : UnaryExpressionNode
    {
        public TypeNode targetCastType;
        public PrimaryExpressionNode exp;

        public CastingExpressionNode(){}

        public CastingExpressionNode(TypeNode targetCastType, PrimaryExpressionNode exp)
        {
            this.targetCastType = targetCastType;
            this.exp = exp;
        }
    }
}