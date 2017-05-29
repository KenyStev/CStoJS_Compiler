namespace Compiler.TreeNodes.Statements
{
    public class ElseStatementNode
    {
        public EmbeddedStatementNode stmts;

        private ElseStatementNode(){}
        public ElseStatementNode(EmbeddedStatementNode stmts)
        {
            this.stmts = stmts;
        }
    }
}