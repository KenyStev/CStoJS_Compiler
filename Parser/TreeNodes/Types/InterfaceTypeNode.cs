using System;
using System.Collections.Generic;

namespace Compiler.TreeNodes.Types
{
    public class InterfaceTypeNode : TypeNode
    {
        private IdNode name;
        private List<MethodHeaderNode> methodDeclarationList;
        private List<IdNode> inheritanceses;

        public InterfaceTypeNode(IdNode name, List<MethodHeaderNode> methodDeclarationList)
        {
            this.name = name;
            this.methodDeclarationList = methodDeclarationList;
        }

        public void setInheritance(List<IdNode> inheritanceses)
        {
            this.inheritanceses = inheritanceses;
        }
    }
}