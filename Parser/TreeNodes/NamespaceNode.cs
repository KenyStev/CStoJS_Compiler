using System;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.TreeNodes
{
    public class NamespaceNode
    {
        private IdNode name;
        private List<UsingNode> usingDirectives;
        private List<TypeNode> typesDeclarations;

        public NamespaceNode(IdNode name)
        {
            this.name = name;
            this.typesDeclarations = new List<TypeNode>();
        }

        public void setUsings(List<UsingNode> namespaceDirectives)
        {
            this.usingDirectives = namespaceDirectives;
        }

        public void addTypeList(List<TypeNode> listTypeDeclared)
        {
            this.typesDeclarations = this.typesDeclarations.Union(listTypeDeclared).ToList();
        }
    }
}