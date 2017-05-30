using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Compiler.TreeNodes.Types;
using Compiler.TreeNodes.Expressions.UnaryExpressions;

namespace Compiler.TreeNodes
{
    public class NamespaceNode
    {
        [XmlElement(typeof(IdNode))]
        public IdNode Identifier;

        [XmlArray("UsingDirectives"),
        XmlArrayItem("Directive")]
        public List<UsingNode> usingDirectives;

        [XmlElement(typeof(TypeNode)),
        XmlElement(typeof(ClassTypeNode),ElementName = "Class"),
        XmlElement(typeof(InterfaceTypeNode),ElementName = "Interface"),
        XmlElement(typeof(EnumTypeNode),ElementName = "Enum")]
        public List<TypeNode> typesDeclarations;
        public Token token;

        private NamespaceNode(){}
        public NamespaceNode(IdNode name,Token namespaceToken)
        {
            this.Identifier = name;
            this.typesDeclarations = new List<TypeNode>();
            this.token = namespaceToken;
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