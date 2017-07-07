using Compiler.TreeNodes.Expressions;

namespace Compiler.TreeNodes.Statements
{
    public abstract class JumpStatementNode : EmbeddedStatementNode
    {
        public JumpStatementNode(){}
        public JumpStatementNode(Token token) : base(token)
        {
        }
    }
}