namespace Compiler.TreeNodes.Statements
{
    public abstract class EmbeddedStatementNode : StatementNode
    {
        public EmbeddedStatementNode(){}
        public EmbeddedStatementNode(Token token) : base(token){}
    }
}