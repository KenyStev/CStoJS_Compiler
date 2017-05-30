using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class CastingExpressionNode : UnaryExpressionNode
    {
        public TypeNode targetCastType;
        public PrimaryExpressionNode exp;

        public CastingExpressionNode(){}

        public CastingExpressionNode(TypeNode targetCastType, PrimaryExpressionNode exp,Token token) : base(token)
        {
            this.targetCastType = targetCastType;
            this.exp = exp;
        }
    }
}