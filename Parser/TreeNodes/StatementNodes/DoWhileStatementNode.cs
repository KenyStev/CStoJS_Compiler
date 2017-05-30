using Compiler.TreeNodes.Expressions;

namespace Compiler.TreeNodes.Statements
{
    public class DoWhileStatementNode : IterationStatementNode
    {
        public ExpressionNode expression;
        public EmbeddedStatementNode body;

        private DoWhileStatementNode(){}
        public DoWhileStatementNode(ExpressionNode exp, EmbeddedStatementNode body,Token token) : base(token)
        {
            this.expression = exp;
            this.body = body;
        }
    }
}