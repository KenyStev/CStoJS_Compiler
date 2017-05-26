using Compiler.TreeNodes.Expressions;

namespace Compiler.TreeNodes
{
    public class EnumNode
    {
        private IdNode name;
        private ExpressionNode value;

        public EnumNode(IdNode name, ExpressionNode value)
        {
            this.name = name;
            this.value = value;
        }
    }
}