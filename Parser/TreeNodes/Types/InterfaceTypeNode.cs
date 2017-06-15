using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Expressions.UnaryExpressions;

namespace Compiler.TreeNodes.Types
{
    public class InterfaceTypeNode : TypeNode
    {
        [XmlArray("MethodHeaders"),
        XmlArrayItem("MethodHeader")]
        public List<MethodHeaderNode> methodDeclarationList;

        [XmlArray("Inheritanceses"),
        XmlArrayItem("BaseItem")]
        public List<IdNode> inheritanceses;
        public Dictionary<string,TypeNode> parents;
        public Dictionary<string, MethodHeaderNode> methods;

        private InterfaceTypeNode(){}
        public InterfaceTypeNode(IdNode name, List<MethodHeaderNode> methodDeclarationList,Token token)
        {
            this.Identifier = name;
            this.methodDeclarationList = methodDeclarationList;
            this.token = token;
            this.parents = new Dictionary<string, TypeNode>();
        }

        public void setInheritance(List<IdNode> inheritanceses)
        {
            this.inheritanceses = inheritanceses;
        }

        public override void Evaluate(API api)
        {
            var thisDeclarationPath = api.getDeclarationPathForType(this);
            if(!Utils.isValidEncapsulation(this.encapsulation,TokenType.RW_PUBLIC))
                Utils.ThrowError("Elements defined in a namespace cannot be declared as private or protected; Interface: "
                +this.Identifier.Name+" ("+thisDeclarationPath+") ");
            NamespaceNode myNs = api.getNamespaceForType(this);
            checkInheritance(api,myNs);
            verifyCycleInheritance(api,myNs);
            checkMethods(api,myNs);
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

        private void checkMethods(API api,NamespaceNode myNs)
        {
            var thisDeclarationPath = api.getDeclarationPathForType(this);
            this.methods = new Dictionary<string,MethodHeaderNode>();
            if(methodDeclarationList==null)return;
            foreach (var methodDe in methodDeclarationList)
            {
                string methodName = api.buildMethodName(methodDe);
                if(this.methods.ContainsKey(methodName))
                    Utils.ThrowError("Type '"+Identifier.Name+"' already defines a member called '"+
                    methodDe.Identifier.Name+"' with the same parameter types ["+myNs.Identifier.Name+"]("
                    +thisDeclarationPath+")");
                this.methods[methodName] = methodDe;
                api.checkFixedParameters(methodDe,myNs);
            }
        }

        private void checkInheritance(API api,NamespaceNode myNs)
        {
            var thisDeclarationPath = api.getDeclarationPathForType(this);
            if(inheritanceses!=null)
            {
                var usingDirectivesList = myNs.usingDirectivesList();
                foreach (var parent in inheritanceses)
                {
                    TypeNode parentTypeNode = api.getTypeForIdentifier(parent.Name,usingDirectivesList,myNs);
                    if(parentTypeNode==null)
                        Utils.ThrowError("The type or namespace name '"+ parent.Name 
                        +"' could not be found (are you missing a using directive or an assembly reference?) ["
                        +myNs.Identifier.Name+"]");
                    if(!(parentTypeNode is InterfaceTypeNode))
                        Utils.ThrowError("Type '"+ parentTypeNode.Identifier.Name 
                        +"' in interface list is not an interface ["+ myNs.Identifier.Name +"]("
                        +thisDeclarationPath+")");
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
            // return "InterfaceType";
        }

        public override bool Equals(object obj)
        {
            if(obj is InterfaceTypeNode)
            {
                var o = obj as InterfaceTypeNode;
                return Identifier.Name == o.Identifier.Name;
            }
            return false;
        }
    }
}