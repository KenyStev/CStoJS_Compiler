using System;
using System.Collections.Generic;
using Compiler.SemanticAPI;
using Compiler.Writer;

namespace Compiler.TreeNodes.Statements
{
    public class ForInitializerNode
    {
        public LocalVariableDeclarationNode localVariables;
        public List<StatementExpressionNode> statementExpresions;
        public Token token;

        public ForInitializerNode(){}
        public ForInitializerNode(LocalVariableDeclarationNode localVariables,Token token)
        {
            this.localVariables = localVariables;
            this.statementExpresions = null;
            this.token = token;
        }

        public ForInitializerNode(List<StatementExpressionNode> stmtsExpList,Token token)
        {
            this.localVariables = null;
            this.statementExpresions = stmtsExpList;
            this.token = token;
        }

        internal void GenerateCode(Writer.Writer writer, API api)
        {
            if(localVariables != null) {
                localVariables.GenerateCode(writer, api);
            }
            else if(localVariables != null) {
                foreach (var stmnt in statementExpresions) {
                    stmnt.GenerateCode(writer, api);
                }
            }
        }
    }
}