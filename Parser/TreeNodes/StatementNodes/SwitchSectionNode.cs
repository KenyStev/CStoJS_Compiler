using System.Collections.Generic;

namespace Compiler.TreeNodes.Statements
{
    public class SwitchSectionNode
    {
        public List<CaseNode> switchLabels;
        public List<StatementNode> stmts;
        public Token token;

        private SwitchSectionNode(){}
        public SwitchSectionNode(List<CaseNode> switchLabels, List<StatementNode> stmts,Token token)
        {
            this.switchLabels = switchLabels;
            this.stmts = stmts;
            this.token = token;
        }
    }
}