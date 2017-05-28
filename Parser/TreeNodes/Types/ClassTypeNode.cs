using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Compiler.TreeNodes.Types
{
    public class ClassTypeNode : TypeNode
    {
        [XmlElement(typeof(IdNode))]
        public IdNode Identifier;

        [XmlArray("Inheritanceses"),
        XmlArrayItem("BaseItem")]
        public List<IdNode> inheritanceses;

        [XmlAttribute(AttributeName = "IsAbstract")]
        public bool IsAbstract;

        [XmlArray("Fields"),
        XmlArrayItem("Field")]
        public List<FieldNode> Fields;

        [XmlArray("Constructors"),
        XmlArrayItem("Contructor")]
        public List<ConstructorNode> Constructors;

        [XmlArray("Methods"),
        XmlArrayItem("Method")]
        public List<MethodNode> Methods;

        private ClassTypeNode()
        {
            Identifier = null;
            inheritanceses = null;
            IsAbstract = false;
        }

        public ClassTypeNode(IdNode identifier)
        {
            this.Identifier = identifier;
            inheritanceses = null;
            Fields = new List<FieldNode>();
            Constructors = new List<ConstructorNode>();
            Methods = new List<MethodNode>();
        }

        public void setInheritance(List<IdNode> inheritanceses)
        {
            this.inheritanceses = inheritanceses;
        }

        public void setAbstract(bool isAbstract)
        {
            this.IsAbstract = isAbstract;
        }

        public void addMethod(MethodNode methodDeclared)
        {
            this.Methods.Add(methodDeclared);
        }

        public void addFields(List<FieldNode> fieldDeclarationList)
        {
            this.Fields.AddRange(fieldDeclarationList);
        }
    }
}