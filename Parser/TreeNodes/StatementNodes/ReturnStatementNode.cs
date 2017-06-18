using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Expressions;

namespace Compiler.TreeNodes.Statements
{
    public class ReturnStatementNode : JumpStatementNode
    {
        public ExpressionNode expression;

        public ReturnStatementNode(){}
        public ReturnStatementNode(ExpressionNode exp, Token token) : base(token)
        {
            this.expression = exp;
        }

        public override void Evaluate(API api)
        {
            throw new NotImplementedException();
        }
    }
}