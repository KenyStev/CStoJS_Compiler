using System;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.TreeNodes
{
    public class CompilationUnitNode
    {
        private List<UsingNode> usingDirectives;
        public NamespaceNode defaultNamespace;
        private List<NamespaceNode> namespaceDeclared;

        public CompilationUnitNode(List<UsingNode> usingList)
        {
            this.usingDirectives = usingList;
            this.defaultNamespace = new NamespaceNode(new IdNode("default"));
            this.namespaceDeclared = new List<NamespaceNode>();
        }

        public void addNamespace(NamespaceNode namespaceDeclared)
        {
            this.namespaceDeclared.Insert(0,namespaceDeclared);
        }
    }
}