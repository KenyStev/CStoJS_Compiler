using Compiler.TreeNodes.Expressions;

namespace Compiler.TreeNodes.Statements
{
    public class JumpStatementNode : EmbeddedStatementNode
    {
        public TokenType type;
        public ExpressionNode expression;

        private JumpStatementNode(){}
        public JumpStatementNode(TokenType type, ExpressionNode exp)
        {
            this.type = type;
            this.expression = exp;
        }
    }
}