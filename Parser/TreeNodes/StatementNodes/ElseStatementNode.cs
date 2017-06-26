using System;
using Compiler.SemanticAPI;
using Compiler.SemanticAPI.ContextUtils;

namespace Compiler.TreeNodes.Statements
{
    public class ElseStatementNode
    {
        public EmbeddedStatementNode statements;
        private Token token;

        private ElseStatementNode(){}
        public ElseStatementNode(EmbeddedStatementNode stmts,Token token)
        {
            this.statements = stmts;
            this.token = token;
        }

        public void Evaluate(API api)
        {
            api.contextManager.pushContext(api.buildContext("else:"+token.getLine(),ContextType.SELECTION,null));
            statements.Evaluate(api);
            api.contextManager.popContext();
        }

        public void GenerateCode(Writer.Writer Writer, API api) {
            statements.GenerateCode(Writer, api);
        }
    }
}