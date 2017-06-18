using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Expressions;

namespace Compiler.TreeNodes.Statements
{
    public class SwitchStatementNode : SelectionStatementNode
    {
        public ExpressionNode expression;
        public SwitchBodyNode switchBodyNode;

        private SwitchStatementNode(){}
        public SwitchStatementNode(ExpressionNode exp, SwitchBodyNode switchBodyNode,Token token) : base(token)
        {
            this.expression = exp;
            this.switchBodyNode = switchBodyNode;
        }

        public override void Evaluate(API api)
        {
            throw new NotImplementedException();
        }
    }
}