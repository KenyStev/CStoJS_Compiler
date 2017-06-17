namespace Compiler.TreeNodes.Expressions.AdditiveExpressions
{
    public abstract class AdditiveExpressionNode : BinaryOperatorNode
    {
        public AdditiveExpressionNode(){}
        public AdditiveExpressionNode(ExpressionNode leftExpression, 
        ExpressionNode multExpression,Token token) : base(leftExpression,multExpression,token)
        {
            
        }
    }
}