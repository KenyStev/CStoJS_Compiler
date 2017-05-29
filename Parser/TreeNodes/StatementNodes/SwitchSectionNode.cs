using System.Collections.Generic;

namespace Compiler.TreeNodes.Statements
{
    public class SwitchSectionNode
    {
        public List<CaseNode> switchLabels;
        public List<StatementNode> stmts;

        private SwitchSectionNode(){}
        public SwitchSectionNode(List<CaseNode> switchLabels, List<StatementNode> stmts)
        {
            this.switchLabels = switchLabels;
            this.stmts = stmts;
        }
    }
}