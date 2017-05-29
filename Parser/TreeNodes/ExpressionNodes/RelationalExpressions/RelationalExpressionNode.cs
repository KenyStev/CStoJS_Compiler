namespace Compiler.TreeNodes.Expressions.RelationalExpressions
{
    public class RelationalExpressionNode : BinaryOperatorNode
    {
        public ExpressionNode leftExpression;
        public ExpressionNode shiftExpression;

        public RelationalExpressionNode(){}

        public RelationalExpressionNode(ExpressionNode leftExpression, ExpressionNode shiftExpression)
        {
            this.leftExpression = leftExpression;
            this.shiftExpression = shiftExpression;
        }
        
    }
}