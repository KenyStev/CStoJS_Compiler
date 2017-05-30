using Compiler.TreeNodes.Expressions;

namespace Compiler.TreeNodes.Statements
{
    public class WhileStatementNode : IterationStatementNode
    {
        public ExpressionNode expression;
        public EmbeddedStatementNode body;

        private WhileStatementNode(){}
        public WhileStatementNode(ExpressionNode exp, EmbeddedStatementNode body,Token token) : base(token)
        {
            this.expression = exp;
            this.body = body;
        }
    }
}