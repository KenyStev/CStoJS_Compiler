using System.Collections.Generic;

namespace Compiler.TreeNodes.Types
{
    public class InterfaceTypeNode : TypeNode
    {
        private IdNode name;
        private List<MethodNode> methodDeclarationList;

        public InterfaceTypeNode(IdNode name, List<MethodNode> methodDeclarationList)
        {
            this.name = name;
            this.methodDeclarationList = methodDeclarationList;
        }
    }
}