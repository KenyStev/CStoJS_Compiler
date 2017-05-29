using System.Collections.Generic;

namespace Compiler.TreeNodes.Statements
{
    public class StatementBlockNode
    {
        public List<StatementNode> statements;

        private StatementBlockNode()
        {
            statements = null;
        }
        public StatementBlockNode(List<StatementNode> statements)
        {
            this.statements = statements;
        }
    }
}