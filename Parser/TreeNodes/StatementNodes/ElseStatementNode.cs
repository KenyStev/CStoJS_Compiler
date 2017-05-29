namespace Compiler.TreeNodes.Statements
{
    public class ElseStatementNode
    {
        public EmbeddedStatementNode statements;

        private ElseStatementNode(){}
        public ElseStatementNode(EmbeddedStatementNode stmts)
        {
            this.statements = stmts;
        }
    }
}