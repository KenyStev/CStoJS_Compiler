using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Compiler.TreeNodes.Types;
using Compiler.TreeNodes.Expressions.UnaryExpressions;
using Compiler.SemanticAPI;

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
        public NamespaceNode parentNamespace;

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

        public void setParentNamePrefix(IdNode identifier)
        {
            this.Identifier = getFullNamespaceName(identifier, this.Identifier);
        }

        private IdNode getFullNamespaceName(IdNode father, IdNode id)
        {
            string fatherName = father.Name;
            foreach(var a in father.attributes)
            {
                fatherName += "."+a.Name;
            }
            string nsName = fatherName + "." + id.Name;
            foreach(var a in id.attributes)
            {
                nsName += "."+a.Name;
            }
            return new IdNode(nsName,id.token);
        }

        public List<string> usingDirectivesList()
        {
            List<string> usings = new List<string>();
            if(usingDirectives!=null)
            {
                foreach (var usingDirective in usingDirectives)
                {
                    if(!usings.Contains(usingDirective.Identifier.Name))
                        usings.Add(usingDirective.Identifier.Name);
                }
            }
            return usings;
        }

        public void addParentUsings(List<UsingNode> usingDirectives)
        {
            if(usingDirectives!=null)
            {
                if(this.usingDirectives==null)
                    this.usingDirectives = new List<UsingNode>();
                this.usingDirectives.AddRange(usingDirectives);
            }
        }

        public void setParentNamespace(ref NamespaceNode currentNamespace)
        {
            this.parentNamespace = currentNamespace;
        }

        public void Evaluate(API api)
        {
            foreach (var typeDef in typesDeclarations)
            {
                try{
                    typeDef.Evaluate(api);
                }catch(NotImplementedException ex){
                    Console.WriteLine("TODO: ["+typeDef.GetType().Name+"]("+ex.Message+")");
                }
            }
        }
    }
}