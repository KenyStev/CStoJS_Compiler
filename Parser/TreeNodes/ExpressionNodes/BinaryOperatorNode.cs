namespace Compiler.TreeNodes.Expressions
{
    public class BinaryOperatorNode : ExpressionNode
    {
        public ExpressionNode leftOperand;
        public ExpressionNode rightOperand;

        public BinaryOperatorNode(){} 
        public BinaryOperatorNode(ExpressionNode leftExpression, ExpressionNode relationalExpression)
        {
            this.leftOperand = leftExpression;
            this.rightOperand = relationalExpression;
        }
    }
}