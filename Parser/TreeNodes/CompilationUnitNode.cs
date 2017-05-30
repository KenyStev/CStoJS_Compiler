using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Compiler.TreeNodes.Expressions.UnaryExpressions;

namespace Compiler.TreeNodes
{
    public class CompilationUnitNode
    {
        [XmlArray("UsingDirectives"),
        XmlArrayItem("Directive")]
        public List<UsingNode> usingDirectives;

        [XmlElement(typeof(NamespaceNode))]
        public NamespaceNode defaultNamespace;

        [XmlArray("Namespaceses"),
        XmlArrayItem("Namespace")]
        public List<NamespaceNode> namespaceDeclared;

        private CompilationUnitNode(){}
        public CompilationUnitNode(List<UsingNode> usingList)
        {
            this.usingDirectives = usingList;
            this.defaultNamespace = new NamespaceNode(new IdNode("default",null),null);
            this.namespaceDeclared = new List<NamespaceNode>();
        }

        public void addNamespace(NamespaceNode namespaceDeclared)
        {
            this.namespaceDeclared.Insert(0,namespaceDeclared);
        }
    }
}