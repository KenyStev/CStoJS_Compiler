using Compiler.TreeNodes.Expressions;

namespace Compiler.TreeNodes.Statements
{
    public class IfStatementNode : SelectionStatementNode
    {
        public ExpressionNode expression;
        public EmbeddedStatementNode statements;
        public ElseStatementNode elseBock;

        private IfStatementNode(){} 
        public IfStatementNode(ExpressionNode exp, EmbeddedStatementNode stmts, ElseStatementNode elseBock)
        {
            this.expression = exp;
            this.statements = stmts;
            this.elseBock = elseBock;
        }
    }
}