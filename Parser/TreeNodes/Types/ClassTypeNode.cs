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
        public Dictionary<string, TypeNode> parents;

        private ClassTypeNode(){}

        public ClassTypeNode(IdNode identifier,Token token)
        {
            base.Identifier = identifier;
            inheritanceses = null;
            Fields = new List<FieldNode>();
            Constructors = new List<ConstructorNode>();
            Methods = new List<MethodNode>();
            this.token = token;
            this.parents = new Dictionary<string, TypeNode>();
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
            var thisDeclarationPath = api.getDeclarationPathForType(this);
            NamespaceNode myNs = api.getNamespaceForType(this);
            if(!Utils.isValidEncapsulationForClass(this.encapsulation,TokenType.RW_PUBLIC))
                Utils.ThrowError("Elements defined in a namespace cannot be declared as private or protected; Interface: "
                +this.Identifier.Name+" ("+thisDeclarationPath+") ");
            checkInheritance(api,myNs);
            verifyCycleInheritance(api,myNs);
            checkParents(api,myNs);
            checkMethods(api,myNs);
        }

        private void checkMethods(API api, NamespaceNode myNs)
        {
            var path = api.getDeclarationPathForType(this);
            var my_methods = api.getMethodsForType(this,path+" "+Identifier.token.getLine());
            foreach (var method in my_methods)
            {
                var childMethodName = Identifier.Name+"."+api.buildMethodName(method.Value.methodHeaderNode);
                if(!method.Value.evaluated)
                {
                    if((method.Value.Modifier==null || api.validateModifier(method.Value.Modifier,TokenType.RW_VIRTUAL,TokenType.RW_STATIC)) && method.Value.statemetBlock==null)
                        Utils.ThrowError("'"+childMethodName+"' must declare a body because it is not marked abstract ["
                        +myNs.Identifier.Name+"] "+method.Value.token.getLine());
                    
                    if(api.validateModifier(method.Value.Modifier,TokenType.RW_ABSTRACT))
                    {
                        if(method.Value.statemetBlock!=null)
                            Utils.ThrowError("'"+childMethodName+"' cannot declare a body because it is marked abstract ["
                            +myNs.Identifier.Name+"] "+method.Value.token.getLine());
                        if(Utils.isValidEncapsulationForClass(method.Value.encapsulation,TokenType.RW_PRIVATE))
                            Utils.ThrowError("'"+childMethodName+"' virtual or abstract members cannot be private ["
                            +myNs.Identifier.Name+"] "+method.Value.token.getLine());
                        if(!IsAbstract)
                            Utils.ThrowError("'"+childMethodName+"' is abstract but it is contained in non-abstract class '"
                            +Identifier.Name+"' ["+myNs.Identifier.Name+"] "+method.Value.token.getLine());
                    }else if(api.validateModifier(method.Value.Modifier,TokenType.RW_VIRTUAL))
                    {
                        if(Utils.isValidEncapsulationForClass(method.Value.encapsulation,TokenType.RW_PRIVATE))
                            Utils.ThrowError("'"+childMethodName+"' virtual or abstract members cannot be private ["
                            +myNs.Identifier.Name+"] "+method.Value.token.getLine());
                    }else if(api.validateModifier(method.Value.Modifier,TokenType.RW_OVERRIDE))
                        Utils.ThrowError("Modifier 'override' can't be aplied to the method "+childMethodName+" "+method.Value.token.getLine());
                    method.Value.evaluated=true;
                }
                api.checkFixedParameters(method.Value.methodHeaderNode,myNs);
            }
        }

        private void checkParents(API api, NamespaceNode myNs)
        {
            var path = api.getDeclarationPathForType(this);
            var my_methods = api.getMethodsForType(this,path+" "+Identifier.token.getLine());
            foreach (var parent in parents)
            {
                checkParentMethods(api, parent.Value, my_methods);
            }
        }

        private void checkParentMethods(API api, TypeNode parent, Dictionary<string,MethodNode> my_methods)
        {
            var path = api.getDeclarationPathForType(this);
            var parent_methods = api.getMethodsForType(parent,path+" "+Identifier.token.getLine());
            foreach (var parent_method in parent_methods)
            {
                if(my_methods.ContainsKey(parent_method.Key))
                {
                    api.checkParentMethodOnMe(my_methods[parent_method.Key],parent_method.Value,this,parent,api.getNamespaceForType(this));
                }else if(parent is InterfaceTypeNode || (parent is ClassTypeNode && ((ClassTypeNode)parent).IsAbstract && !IsAbstract))
                {
                    Utils.ThrowError(Identifier.ToString()+" does not implement: "+parent.ToString()
                    +"."+parent_method.Key+" "+Identifier.token.getLine());
                }
            }
        }

        private void verifyCycleInheritance(API api,NamespaceNode myNs)
        {
            var thisDeclarationPath = api.getDeclarationPathForType(this);
            try{
                api.hasCycleInheritance(Identifier.Name,parents);
            }catch(SemanticException ex){
                Utils.ThrowError(thisDeclarationPath+" -> "+ex.Message+" ["+myNs.Identifier.Name+"] "+Identifier.token.getLine());
            }
        }

        private void checkInheritance(API api, NamespaceNode myNs)
        {
            var thisDeclarationPath = api.getDeclarationPathForType(this);
            ClassTypeNode classParent = null;
            if(inheritanceses!=null)
            {
                var usingDirectivesList = myNs.usingDirectivesList();
                foreach (var parent in inheritanceses)
                {
                    TypeNode parentTypeNode = api.getTypeForIdentifier(parent.Name,usingDirectivesList,myNs);
                    if(parentTypeNode==null)
                        Utils.ThrowError("The type or namespace name '"+ parent.Name 
                        +"' could not be found (are you missing a using directive or an assembly reference?) ["
                        +myNs.Identifier.Name+"]: "+parent.token.getLine());
                    if(parents.Count==0)
                    {
                        if(parentTypeNode is ClassTypeNode)
                        {
                            classParent = parentTypeNode as ClassTypeNode;
                            if(!Utils.isValidEncapsulationForClass(parentTypeNode.encapsulation,TokenType.RW_PUBLIC))
                                Utils.ThrowError("Parent '" + parentTypeNode.Identifier.Name 
                                + "' can't be reached due to its encapsulation level. "+parentTypeNode.token.getLine());
                        }
                        else if(!(parentTypeNode is InterfaceTypeNode))
                            Utils.ThrowError("Type '"+ parentTypeNode.Identifier.Name 
                            +"' in interface list is not an interface ["+ myNs.Identifier.Name +"]("
                            +thisDeclarationPath+")");
                    }else{
                        if(parentTypeNode is ClassTypeNode)
                        {
                            if(classParent!=null)
                            {
                                Utils.ThrowError("Class '"+ Identifier.Name 
                                +"' cannot have multiple base classes: '"+classParent.Identifier.Name+"' and '"+
                                parentTypeNode.Identifier.Name+"' ["+myNs.Identifier.Name+"]");
                            }else{
                                Utils.ThrowError("Base class '"+parentTypeNode.Identifier.Name
                                +"' must come before any interfaces ["+ myNs.Identifier.Name +"]");
                            }
                        }
                        else if(!(parentTypeNode is InterfaceTypeNode))
                            Utils.ThrowError("Type '"+ parentTypeNode.Identifier.Name 
                            +"' in interface list is not an interface ["+ myNs.Identifier.Name +"]("
                            +thisDeclarationPath+")");
                    }
                    if(parents.ContainsKey(parentTypeNode.Identifier.Name))
                        Utils.ThrowError("Redundant Inheritance. " + parentTypeNode.Identifier.Name 
                        + " was found twice as inheritance in " 
                        + "["+Identifier.Name+"] " + thisDeclarationPath);
                    this.parents[parentTypeNode.Identifier.Name] = parentTypeNode;
                }
            }
        }

        public override string ToString()
        {
            return Identifier.Name;
            // return "ClassType";
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