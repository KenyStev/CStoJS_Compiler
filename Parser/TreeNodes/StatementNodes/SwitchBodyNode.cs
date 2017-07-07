using System;
using System.Collections.Generic;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

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

        public void Evaluate(API api, TypeNode expType)
        {
            if(switchSections!=null)
                foreach (var sect in switchSections)
                {
                    sect.Evaluate(api,expType);
                }
        }

        public void GenerateCode(Writer.Writer Writer, API api) {
            foreach(var sect in this.switchSections) {
                sect.GenerateCode(Writer, api);
            }
        }
    }
}