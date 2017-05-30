namespace Compiler.TreeNodes.Statements
{
    public abstract class StatementNode
    {
        Token token;
        public StatementNode(){}
        public StatementNode(Token token)
        {
            this.token = token;
        }
    }
}