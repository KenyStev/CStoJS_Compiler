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
            int x = 4;
            // TypeNode x = new BoolTypeNode();
            switch (x)
            {
            //     case new VarTypeNode(null) :
            //         Console.Write("");
            //         break;
               case 5:
                    {Console.Write("");
                    break;}
                    {
                        Console.Write("");
                    break;
                    }
            //     default: break;
            }
            string caseLBL = switchLabels[switchLabels.Count-1].ToString();
            if(switchLabels!=null && stmts==null)
                Utils.ThrowError("Control cannot fall out of switch from final case label "
                +caseLBL+" ["+api.currentNamespace.Identifier.Name+"]");
            
            foreach (var lbl in switchLabels)
            {
                lbl.Evaluate(api,expType);
            }

            if(stmts==null)
                Utils.ThrowError("Control cannot fall out of switch from final case label "
                +caseLBL+" ["+api.currentNamespace.Identifier.Name+"]");
            
            int breakCount = 0;
            foreach (var stmt in stmts)
            {
                if(stmt is BreakStatementNode)
                    breakCount++;
                stmt.Evaluate(api);
            }
            if(breakCount==0)
                Utils.ThrowError("Control cannot fall out of switch from final case label "
                +caseLBL+" ["+api.currentNamespace.Identifier.Name+"]");
        }
    }
}