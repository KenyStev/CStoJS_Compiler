using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Expressions.UnaryExpressions;

namespace Compiler.TreeNodes.Types
{
    public class ClassTypeNode : TypeNode
    {
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

        private ClassTypeNode(){}

        public ClassTypeNode(IdNode identifier,Token token)
        {
            base.Identifier = identifier;
            inheritanceses = null;
            Fields = new List<FieldNode>();
            Constructors = new List<ConstructorNode>();
            Methods = new List<MethodNode>();
            this.token = token;
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

        public void addContructor(ConstructorNode contructoreDeclaration)
        {
            this.Constructors.Add(contructoreDeclaration);
        }

        public override void Evaluate(API api)//TODO
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            // return Identifier.Name;
            return "ClassType";
        }

        public override bool Equals(object obj)
        {
            if(obj is ClassTypeNode)
            {
                var o = obj as ClassTypeNode;
                return Identifier.Name == o.Identifier.Name;
            }
            return false;
        }
    }
}