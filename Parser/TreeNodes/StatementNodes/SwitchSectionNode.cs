using System;
using System.Collections.Generic;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

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

        public void Evaluate(API api, TypeNode expType)
        {
            // int x = 4;
            // switch (x)
            // {
            //     case 4:
            //         Console.Write("");
            //         break;
            //     case 5:
            //         {Console.Write("");
            //         break;}
            //         {Console.Write("");
            //         }
            //     default: break;
            // }
            throw new NotImplementedException();
        }
    }
}