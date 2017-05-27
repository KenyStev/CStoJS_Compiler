using System;
using System.Collections.Generic;
using System.Xml.Serialization;

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

        private InterfaceTypeNode()
        {
            Identifier = null;
            methodDeclarationList = null;
            inheritanceses = null;
        }
        public InterfaceTypeNode(IdNode name, List<MethodHeaderNode> methodDeclarationList)
        {
            this.Identifier = name;
            this.methodDeclarationList = methodDeclarationList;
        }

        public void setInheritance(List<IdNode> inheritanceses)
        {
            this.inheritanceses = inheritanceses;
        }
    }
}