using System;
using System.Collections.Generic;

namespace Compiler.TreeNodes.Statements
{
    public class ForInitializerNode
    {
        public LocalVariableDeclarationNode localVariables;
        private List<StatementExpressionNode> statementExpresions;

        public ForInitializerNode(){
            this.localVariables = null;
            this.statementExpresions = null;
        }
        public ForInitializerNode(LocalVariableDeclarationNode localVariables)
        {
            this.localVariables = localVariables;
            this.statementExpresions = null;
        }

        public void setStatements(List<StatementExpressionNode> stmtsExpList)
        {
            this.statementExpresions = stmtsExpList;
        }
    }
}