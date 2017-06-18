using System;
using System.Collections.Generic;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Expressions;

namespace Compiler.TreeNodes.Statements
{
    public class ForStatementNode : IterationStatementNode
    {
        public ForInitializerNode Initializer;
        public ExpressionNode expression;
        public List<StatementExpressionNode> postIncrementStmts;
        public EmbeddedStatementNode StatementBlock;

        private ForStatementNode(){}
        public ForStatementNode(ForInitializerNode forInitializer, ExpressionNode exp, 
        List<StatementExpressionNode> postIncrementStmts, EmbeddedStatementNode stmts,Token token) : base(token)
        {
            this.Initializer = forInitializer;
            this.expression = exp;
            this.postIncrementStmts = postIncrementStmts;
            this.StatementBlock = stmts;
        }

        public override void Evaluate(API api)
        {
            throw new NotImplementedException();
        }
    }
}