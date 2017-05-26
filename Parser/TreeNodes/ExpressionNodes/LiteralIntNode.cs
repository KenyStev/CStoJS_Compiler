namespace Compiler.TreeNodes.Expressions
{
    public class LiteralIntNode : ExpressionNode
    {
        private int IntValue;

        public LiteralIntNode(int IntValue)
        {
            this.IntValue = IntValue;
        }
    }
}