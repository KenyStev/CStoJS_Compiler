using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Compiler.SemanticAPI;
using Compiler.SemanticAPI.ContextUtils;
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
            evaluated = false;
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
            if(evaluated) return;
            Console.WriteLine("evaluating: "+ToString());
            if(ToString()=="Rectangulo")
                Console.Write("");
            var thisDeclarationPath = api.getDeclarationPathForType(this);
            NamespaceNode myNs = api.getNamespaceForType(this);
            if(!Utils.isValidEncapsulationForClass(this.encapsulation,TokenType.RW_PUBLIC))
                Utils.ThrowError("Elements defined in a namespace cannot be declared as private or protected; Interface: "
                +this.Identifier.Name+" ("+thisDeclarationPath+") ");
            checkInheritance(api,myNs);
            verifyCycleInheritance(api,myNs);
            checkParents(api,myNs);
            api.contextManager.pushContext(api.buildContextForTypeDeclaration(this));
            checkFields(api,myNs);
            checkConstructors(api,myNs);
            checkMethods(api,myNs);
            api.contextManager.backContextToObject();
            this.evaluated = true;
        }

        public bool checkRelationWith(API api, TypeNode target)
        {
            if(target.Identifier.Equals(Identifier) && api.getNamespaceForType(target).Equals(api.getNamespaceForType(this)))
                return true;
            if(parents!=null)
            foreach (var item in parents)
            {
                if(item.Value is ClassTypeNode)
                {
                    if(target.Identifier.Equals(item.Value.Identifier) && api.getNamespaceForType(target).Equals(api.getNamespaceForType(item.Value)))
                        return true;
                    bool found = ((ClassTypeNode)item.Value).checkRelationWith(api,target);
                    if(found)return true;
                }
            }
            return false;
        }

        private void checkFields(API api, NamespaceNode myNs)
        {
            Console.WriteLine("evaluating: checkFields");
            var path = api.getDeclarationPathForType(this);
            Dictionary<string,FieldNode> my_fields = api.getFieldsForClass(this,path+" "+Identifier.token.getLine());
            foreach (var field in my_fields)
            {
                var f = field.Value;
                if(f.type is VoidTypeNode)
                    Utils.ThrowError("Field cannot have void type ["+myNs.Identifier.Name+"] "+f.type.token.getLine());
                var typeName =  Utils.getNameForType(f.type);
                var typeNode = api.getTypeForIdentifier(typeName);
                if(typeNode==null)
                    Utils.ThrowError("The type or namespace name '"+ typeName 
                    +"' could not be found (are you missing a using directive or an assembly reference?) ["
                    +myNs.Identifier.Name+"]: "+f.type.token.getLine());
                if(typeNode is VoidTypeNode)
                    Utils.ThrowError("The type '" + typeNode.ToString()+ "' is not valid for field " 
                    + field.Value.identifier.Name.ToString() + " in class " + Identifier.Name.ToString() 
                    + ". "+ f.type.token.getLine());
                if(typeNode is ClassTypeNode && Utils.isValidEncapsulationForClass(((ClassTypeNode)typeNode).encapsulation,TokenType.RW_PRIVATE))
                    Utils.ThrowError("The type '" + typeName + "' can't be reached due to encapsulation level. "+ f.type.token.getLine());
                
                // if (f.type is ArrayTypeNode)//CHANGE TYPE
                //     ((ArrayTypeNode)f.type).DataType = typeNode;
                // else
                //     f.type = typeNode;
            }
            checkFieldsAssignment(api,my_fields,myNs);
        }

        private void checkFieldsAssignment(API api, Dictionary<string, FieldNode> my_fields, NamespaceNode myNs)
        {
            Console.WriteLine("evaluating: checkFieldsAssignment");
            foreach (var field in my_fields)
            {
                if(field.Value.assigner!=null)
                {
                    var f = field.Value.type;//api.getTypeForIdentifier(field.Value.type.ToString());
                    if(f==null)
                        Console.Write("");
                    var assigner = field.Value.assigner;
                    TypeNode typeAssignmentNode = assigner.EvaluateType(api,null,true);
                    
                    var tdn = typeAssignmentNode;
                    var t = (f is AbstractTypeNode)?api.getTypeForIdentifier(Utils.getNameForType(f)):f;
                    string rule = f.ToString() + "," + typeAssignmentNode.ToString();
                    string rule2 = (t is AbstractTypeNode)?"":t.getComparativeType() + "," + typeAssignmentNode.ToString();
                    string rule3 = (t is AbstractTypeNode)?"":t.getComparativeType() + "," + typeAssignmentNode.getComparativeType();
                     if (!api.assignmentRules.Contains(rule)
                        && !api.assignmentRules.Contains(rule2)
                        && !api.assignmentRules.Contains(rule3)
                        && f.ToString() != typeAssignmentNode.ToString()
                        && !f.Equals(typeAssignmentNode))
                    {
                        f = (f is AbstractTypeNode)?api.getTypeForIdentifier(Utils.getNameForType(f)):f;
                        if(f.getComparativeType() == Utils.Class && tdn.getComparativeType() == Utils.Class)
                        {
                            if(!api.checkRelationBetween(f, tdn))
                                Utils.ThrowError("1Not a valid assignment. Trying to assign " + tdn.ToString() + " to field with type " + f.ToString()+" "+ field.Value.identifier.token.getLine());
                        }else if ((!(f.getComparativeType() == Utils.Class || f.getComparativeType() == Utils.String) && tdn is NullTypeNode))
                        {
                            Utils.ThrowError("2Not a valid assignment. Trying to assign " + tdn.ToString() + " to field with type " + f.ToString()+" "+ field.Value.identifier.token.getLine());
                        }
                        else
                            Utils.ThrowError("3Not a valid assignment. Trying to assign " + tdn.ToString() + " to field with type " + f.ToString()+" "+ field.Value.identifier.token.getLine());
                    }
                }
            }
        }

        private void checkConstructors(API api, NamespaceNode myNs)
        {
            Console.WriteLine("evaluating: checkConstructors");
            var path = api.getDeclarationPathForType(this);
            var my_constructors = api.getConstructorsForClass(this,path+" "+Identifier.token.getLine());
            foreach (var ct in my_constructors)
            {
                api.checkFixedParameters(ct.Value.parameters,myNs);
                if(ct.Value.statementBlock==null)
                    Utils.ThrowError("Constructor has no body. "+ct.Value.identifier.token.getLine());
            }
            checkConstructorsBody(api,my_constructors,myNs);
        }

        private void checkConstructorsBody(API api, Dictionary<string, ConstructorNode> my_constructors, NamespaceNode myNs)
        {
            Console.WriteLine("evaluating: checkConstructorsBody");
            foreach (var ctror in my_constructors)
            {
                api.contextManager.pushContext(api.buildContextForConstructor(ctror));
                ctror.Value.statementBlock.Evaluate(api);

                List<string> argsTypes = new List<string>();
                if(ctror.Value.initializer!=null)
                    foreach (var arg in ctror.Value.initializer.arguments)
                    {
                        var Typearg = arg.expression.EvaluateType(api,null,true);
                        argsTypes.Add(Typearg.ToString());
                    }
                if(!api.contextManager.existBaseConstructor(argsTypes))
                    Utils.ThrowError("'"+Identifier.Name+"' does not contain a constructor that takes "+argsTypes.Count
                    +" arguments ["+api.currentNamespace.Identifier.Name+"]");
                api.contextManager.popContext();
            }
        }

        private void checkMethods(API api, NamespaceNode myNs)
        {
            Console.WriteLine("evaluating: checkMethods");
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
                api.checkFixedParametersForMethod(method.Value.methodHeaderNode,myNs);
            }
            checkMethodsBody(api, my_methods,myNs);
        }

        private void checkMethodsBody(API api, Dictionary<string, MethodNode> my_methods, NamespaceNode myNs)
        {
            Console.WriteLine("evaluating: checkMethodsBody");
            foreach (var method in my_methods)
            {
                api.contextManager.pushContext(api.buildContextForMethod(method));
                if(method.Value.statemetBlock!=null)
                    method.Value.statemetBlock.Evaluate(api);
                api.contextManager.popContext();
            }
        }

        private void checkParents(API api, NamespaceNode myNs)
        {
            Console.WriteLine("evaluating: checkParents");
            var path = api.getDeclarationPathForType(this);
            var my_methods = api.getMethodsForType(this,path+" "+Identifier.token.getLine());
            foreach (var parent in parents)
            {
                checkParentMethods(api, parent.Value, my_methods);
            }
        }

        private void checkParentMethods(API api, TypeNode parent, Dictionary<string,MethodNode> my_methods)
        {
            Console.WriteLine("evaluating: checkParentMethods");
            var path = api.getDeclarationPathForType(this);
            var parent_methods = api.getMethodsForType(parent,path+" "+Identifier.token.getLine());
            foreach (var parent_method in parent_methods)
            {
                if(my_methods.ContainsKey(parent_method.Key))
                {
                    api.checkParentMethodOnMe(my_methods[parent_method.Key],parent_method.Value,this,parent,api.getNamespaceForType(this));
                }else if(parent is InterfaceTypeNode)
                {
                    Utils.ThrowError(Identifier.ToString()+" does not implement: "+parent.ToString()
                    +"."+parent_method.Key+" "+Identifier.token.getLine());
                }else if(parent is ClassTypeNode && ((ClassTypeNode)parent).IsAbstract && !IsAbstract)
                {
                    if(parent_method.Value.statemetBlock==null)
                        Utils.ThrowError(Identifier.ToString()+" does not implement: "+parent.ToString()
                        +"."+parent_method.Key+" "+Identifier.token.getLine());
                }
            }
        }

        private void verifyCycleInheritance(API api,NamespaceNode myNs)
        {
            Console.WriteLine("evaluating: verifyCycleInheritance");
            var thisDeclarationPath = api.getDeclarationPathForType(this);
            try{
                api.hasCycleInheritance(Identifier.Name,parents);
            }catch(SemanticException ex){
                Utils.ThrowError(thisDeclarationPath+" -> "+ex.Message+" ["+myNs.Identifier.Name+"] "+Identifier.token.getLine());
            }
        }

        private void checkInheritance(API api, NamespaceNode myNs)
        {
            Console.WriteLine("evaluating: checkInheritance");
            var thisDeclarationPath = api.getDeclarationPathForType(this);
            ClassTypeNode classParent = null;
            if(inheritanceses!=null && parents.Count==0)
            {
                int parentsCounter = 0;
                foreach (var parent in inheritanceses)
                {
                    if(parent.Name=="ISortable")
                        Console.Write("");
                    TypeNode parentTypeNode = api.getTypeForIdentifier(parent.Name);
                    if(parentTypeNode==null)
                        Utils.ThrowError("The type or namespace name '"+ parent.Name 
                        +"' could not be found (are you missing a using directive or an assembly reference?) ["
                        +myNs.Identifier.Name+"]: "+parent.token.getLine());
                    if(parentsCounter==0)
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
                        parentsCounter++;
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
        }

        public string getDefinitionType()
        {
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

        public override string getComparativeType()
        {
            string[] primitives = { Utils.Bool, Utils.Char, Utils.Float, Utils.String, Utils.Int, Utils.Void, Utils.Var };
            foreach (string s in primitives)
            {
                if (Identifier.token.lexeme == s)
                    return s;
            }
            return Utils.Class;
        }
    }
}