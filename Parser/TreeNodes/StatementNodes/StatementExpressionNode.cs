using Compiler.TreeNodes.Expressions;

namespace Compiler.TreeNodes.Statements
{
    public class StatementExpressionNode : EmbeddedStatementNode
    {
        public ExpressionNode expressionNode;

        public StatementExpressionNode(){}
        public StatementExpressionNode(ExpressionNode expressionNode)
        {
            this.expressionNode = expressionNode;
        }
    }
}