using System;
using System.Collections.Generic;
using Compiler.SemanticAPI;

namespace Compiler.TreeNodes.Statements
{
    public class StatementBlockNode : EmbeddedStatementNode
    {
        public List<StatementNode> statements;

        public StatementBlockNode(){}
        public StatementBlockNode(List<StatementNode> statements,Token token) : base(token)
        {
            this.statements = statements;
        }

        public override void Evaluate(API api)
        {
            if(statements!=null)
            foreach (var stmt in statements)
            {
                stmt.Evaluate(api);
            }
        }
    }
}