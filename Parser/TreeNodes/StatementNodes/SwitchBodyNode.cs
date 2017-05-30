using System.Collections.Generic;

namespace Compiler.TreeNodes.Statements
{
    public class SwitchBodyNode
    {
        public List<SwitchSectionNode> switchSections;
        public Token token;

        private SwitchBodyNode(){}
        public SwitchBodyNode(List<SwitchSectionNode> switchSections,Token token)
        {
            this.switchSections = switchSections;
            this.token = token;
        }
    }
}