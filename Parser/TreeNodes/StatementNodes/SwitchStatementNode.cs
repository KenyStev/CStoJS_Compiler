using Compiler.TreeNodes.Expressions;

namespace Compiler.TreeNodes.Statements
{
    public class SwitchStatementNode : SelectionStatementNode
    {
        public ExpressionNode expression;
        public SwitchBodyNode switchBodyNode;

        private SwitchStatementNode(){}
        public SwitchStatementNode(ExpressionNode exp, SwitchBodyNode switchBodyNode)
        {
            this.expression = exp;
            this.switchBodyNode = switchBodyNode;
        }
    }
}