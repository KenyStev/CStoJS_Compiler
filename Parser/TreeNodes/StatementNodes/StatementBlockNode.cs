using System.Collections.Generic;

namespace Compiler.TreeNodes.Statements
{
    public class StatementBlockNode : EmbeddedStatementNode
    {
        public List<StatementNode> statements;

        private StatementBlockNode(){}
        public StatementBlockNode(List<StatementNode> statements,Token token) : base(token)
        {
            this.statements = statements;
        }
    }
}