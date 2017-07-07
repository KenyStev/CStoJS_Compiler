namespace Compiler.TreeNodes.Statements
{
    public abstract class IterationStatementNode : EmbeddedStatementNode
    {
        public IterationStatementNode(){}
        public IterationStatementNode(Token token) : base(token) {}
    }
}