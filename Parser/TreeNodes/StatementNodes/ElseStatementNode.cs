namespace Compiler.TreeNodes.Statements
{
    public class ElseStatementNode
    {
        public EmbeddedStatementNode statements;
        private Token token;

        private ElseStatementNode(){}
        public ElseStatementNode(EmbeddedStatementNode stmts,Token token)
        {
            this.statements = stmts;
            this.token = token;
        }
    }
}