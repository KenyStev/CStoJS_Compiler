using System.Collections.Generic;

namespace Compiler.TreeNodes
{
    public class MethodHeaderNode
    {
        private ReturnTypeNode returnType;
        private IdNode name;
        private List<ParameterNode> fixedParams;

        public MethodHeaderNode(ReturnTypeNode returnType, IdNode name, List<ParameterNode> fixedParams)
        {
            this.returnType = returnType;
            this.name = name;
            this.fixedParams = fixedParams;
        }
    }
}