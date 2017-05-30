using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Compiler.TreeNodes.Expressions.UnaryExpressions;

namespace Compiler.TreeNodes.Types
{
    public class InterfaceTypeNode : TypeNode
    {
        [XmlElement(typeof(IdNode))]
        public IdNode Identifier;

        [XmlArray("MethodHeaders"),
        XmlArrayItem("MethodHeader")]
        public List<MethodHeaderNode> methodDeclarationList;

        [XmlArray("Inheritanceses"),
        XmlArrayItem("BaseItem")]
        public List<IdNode> inheritanceses;

        private InterfaceTypeNode(){}
        public InterfaceTypeNode(IdNode name, List<MethodHeaderNode> methodDeclarationList,Token token)
        {
            this.Identifier = name;
            this.methodDeclarationList = methodDeclarationList;
            this.token = token;
        }

        public void setInheritance(List<IdNode> inheritanceses)
        {
            this.inheritanceses = inheritanceses;
        }
    }
}