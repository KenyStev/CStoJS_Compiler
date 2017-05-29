using System;
using System.Collections.Generic;

namespace Compiler.TreeNodes.Statements
{
    public class ForInitializerNode
    {
        public LocalVariableDeclarationNode localVariables;
        public List<StatementExpressionNode> statementExpresions;

        public ForInitializerNode(){
            this.localVariables = null;
            this.statementExpresions = null;
        }
        public ForInitializerNode(LocalVariableDeclarationNode localVariables)
        {
            this.localVariables = localVariables;
            this.statementExpresions = null;
        }

        public ForInitializerNode(List<StatementExpressionNode> stmtsExpList)
        {
            this.localVariables = null;
            this.statementExpresions = stmtsExpList;
        }
    }
}