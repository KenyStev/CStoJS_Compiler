namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class GroupedExpressionNode : PrimaryExpressionNode
    {
        public ExpressionNode expression;

        public GroupedExpressionNode(){}

        public GroupedExpressionNode(ExpressionNode exp,Token token) : base(token)
        {
            this.expression = exp;
        }
    }
}