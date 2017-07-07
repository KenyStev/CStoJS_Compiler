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

        public void GenerateCode(Writer.Writer Writer, API api) {
            if(this.switchLabels != null) {
                foreach(var slabel in this.switchLabels) {
                    slabel.GenerateCode(Writer, api);
                }
                foreach(var stmt in this.stmts) {
                   stmt.GenerateCode(Writer, api);
                }
            }
        }
    }
}