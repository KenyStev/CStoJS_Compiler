using System.Collections.Generic;

namespace Compiler.TreeNodes.Statements
{
    public class SwitchBodyNode
    {
        public List<SwitchSectionNode> switchSections;

        private SwitchBodyNode(){}
        public SwitchBodyNode(List<SwitchSectionNode> switchSections)
        {
            this.switchSections = switchSections;
        }
    }
}