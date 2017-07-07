namespace Compiler.TreeNodes.Statements
{
    public abstract class SelectionStatementNode : EmbeddedStatementNode
    {
        public SelectionStatementNode(){}
        public SelectionStatementNode(Token token) : base(token) {}
    }
}