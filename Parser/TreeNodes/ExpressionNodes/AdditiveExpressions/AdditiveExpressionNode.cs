namespace Compiler.TreeNodes.Expressions.AdditiveExpressions
{
    public class AdditiveExpressionNode : BinaryOperatorNode
    {
        public AdditiveExpressionNode(){}
        public AdditiveExpressionNode(ExpressionNode leftExpression, 
        ExpressionNode multExpression,Token token) : base(leftExpression,multExpression,token)
        {
            
        }
    }
}