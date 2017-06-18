using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Expressions;

namespace Compiler.TreeNodes.Statements
{
    public class IfStatementNode : SelectionStatementNode
    {
        public ExpressionNode expression;
        public EmbeddedStatementNode statements;
        public ElseStatementNode elseBock;

        private IfStatementNode(){} 
        public IfStatementNode(ExpressionNode exp, EmbeddedStatementNode stmts, 
        ElseStatementNode elseBock,Token token) : base(token)
        {
            this.expression = exp;
            this.statements = stmts;
            this.elseBock = elseBock;
        }

        public override void Evaluate(API api)
        {
            throw new NotImplementedException();
        }
    }
}