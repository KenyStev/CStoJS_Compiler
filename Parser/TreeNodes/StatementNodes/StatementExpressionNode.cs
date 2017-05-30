using Compiler.TreeNodes.Expressions;

namespace Compiler.TreeNodes.Statements
{
    public class StatementExpressionNode : EmbeddedStatementNode
    {
        public ExpressionNode expressionNode;

        public StatementExpressionNode(){}
        public StatementExpressionNode(ExpressionNode expressionNode,Token token) : base(token)
        {
            this.expressionNode = expressionNode;
        }
    }
}