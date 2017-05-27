using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes
{
    public class ParameterNode
    {
        private TypeNode type;
        private IdNode paramName;

        public ParameterNode(TypeNode type, IdNode paramName)
        {
            this.type = type;
            this.paramName = paramName;
        }
    }
}