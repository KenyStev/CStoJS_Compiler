using System;
using System.Collections.Generic;
using Compiler.SemanticAPI.ContextUtils;
using Compiler.TreeNodes;
using Compiler.TreeNodes.Statements;
using Compiler.TreeNodes.Types;

namespace Compiler.SemanticAPI
{
    public class API
    {
        private Dictionary<string, CompilationUnitNode> trees;
        public ContextManager contextManager;

        public List<string> assignmentRules;
        public NamespaceNode currentNamespace;

        public API(Dictionary<string, CompilationUnitNode> trees)
        {
            this.trees = trees;
            setAssigmentRules();
        }

        public void setAssigmentRules()
        {
            assignmentRules = new List<string>();
            assignmentRules.Add(Utils.Bool + "," + Utils.Bool);
            assignmentRules.Add(Utils.String + "," + Utils.String);
            assignmentRules.Add(Utils.Float + "," + Utils.Int);
            assignmentRules.Add(Utils.Float + "," + Utils.Float);
            assignmentRules.Add(Utils.Float + "," + Utils.Char);
            assignmentRules.Add(Utils.Char + "," + Utils.Char);
            assignmentRules.Add(Utils.Int + "," + Utils.Char);
            assignmentRules.Add(Utils.Int + "," + Utils.Int);
            assignmentRules.Add(Utils.Class + "," + Utils.Null);
            assignmentRules.Add(Utils.String + "," + Utils.Null);
            assignmentRules.Add(Utils.Enum + "," + Utils.Enum);
        }

        public Dictionary<string, FieldNode> getFieldsForContext(List<FieldNode> localVariables)
        {
            Dictionary<string, FieldNode> fields = new Dictionary<string, FieldNode>();
            foreach (var field in localVariables)
            {
                if(fields.ContainsKey(field.identifier.Name) 
                || contextManager.findVariableInCurrentContext(field.identifier.Name)!=null)
                    Utils.ThrowError("A local variable or function named '"+field.identifier.Name
                    +"' is already defined in this scope ["+currentNamespace.Identifier.Name+"]");
                fields[field.identifier.Name] = field;
            }
            return fields;
        }

        public bool checkRelationBetween(TypeNode leftType, TypeNode target)
        {
            bool found = ((ClassTypeNode)leftType).checkRelationWith(this,target);
            bool found2 = ((ClassTypeNode)target).checkRelationWith(this,leftType);
            return found||found2;
        }

        public void setNamespaces(KeyValuePair<string, CompilationUnitNode> tree)
        {
            foreach(var ns in tree.Value.namespaceDeclared)
            {
                Singleton.addNamespace(ns.Identifier.Name,ns);
            }
        }

        public void setTypes(KeyValuePair<string, CompilationUnitNode> tree)
        {
            setTypesForNamespace(tree.Value.defaultNamespace);
            foreach(var ns in tree.Value.namespaceDeclared)
            {
                setTypesForNamespace(ns);
            }
        }

        public void setTypesForNamespace(NamespaceNode ns)
        {
            string typeNameNS = (ns.Identifier.Name=="default")?"":ns.Identifier.Name+".";
            // var typeName = typeNameNS;
            foreach(var newType in ns.typesDeclarations)
            {
                if (newType is ClassTypeNode)
                {
                    var classType = newType as ClassTypeNode;
                    var typeName = typeNameNS + classType.Identifier.Name;
                    if(Singleton.typesTable.ContainsKey(typeName))
                        Utils.ThrowError("Class: "+typeName+"("+getDeclarationPathForType(newType)
                        +") already declared. Declared before here -> "
                        +getDeclarationPathForType(Singleton.typesTable[typeName]));
                    Singleton.addType(typeName,newType);
                }else if (newType is EnumTypeNode)
                {
                    var enumType = newType as EnumTypeNode;
                    var typeName = typeNameNS + enumType.Identifier.Name;
                    if(Singleton.typesTable.ContainsKey(typeName))
                        Utils.ThrowError("Enum: "+typeName+"("+getDeclarationPathForType(newType)
                        +") already declared. Declared before here -> "
                        +getDeclarationPathForType(Singleton.typesTable[typeName]));
                    Singleton.addType(typeName,newType);
                }else if (newType is InterfaceTypeNode)
                {
                    var interfaceType = newType as InterfaceTypeNode;
                    var typeName = typeNameNS + interfaceType.Identifier.Name;
                    if(Singleton.typesTable.ContainsKey(typeName))
                        Utils.ThrowError("Interface: "+typeName+"("+getDeclarationPathForType(newType)
                        +") already declared. Declared before here -> "
                        +getDeclarationPathForType(Singleton.typesTable[typeName]));
                    Singleton.addType(typeName,newType);
                }
            }
        }

        public void setCurrentNamespace(NamespaceNode namespaceNode)
        {
            this.currentNamespace = namespaceNode;
        }

        public void initContext()
        {
            contextManager = new ContextManager(this);
        }

        public Context buildContextForTypeDeclaration(TypeNode typeNode)
        {
            var path = getDeclarationPathForType(typeNode);
            Context newContext = null;
            if(typeNode is ClassTypeNode)
            {
                newContext = new Context( typeNode.Identifier.Name, ContextType.CLASS,
                                    getFieldsForClass(typeNode as ClassTypeNode,path),
                                    getConstructorsForClass(typeNode as ClassTypeNode,path),
                                    getMethodsForType(typeNode,path)
                                    );
                if(newContext.contextName!="Object")
                {
                    var c = typeNode as ClassTypeNode;
                    Context parentContext=null;
                    foreach (var parent in c.parents)
                    {
                        if(parent.Value is ClassTypeNode)
                        {
                            parentContext = buildContextForTypeDeclaration(parent.Value);
                            break;
                        }
                    }
                    newContext.parentContext = parentContext;
                }
            }else if(typeNode is InterfaceTypeNode)
            {
                newContext = new Context( typeNode.Identifier.Name, ContextType.INTERFACE,null,null,
                                    getMethodsForType(typeNode,path)
                                    );
                if(newContext.contextName!="Object")
                {
                    var c = typeNode as InterfaceTypeNode;
                    if(c.parents!=null && c.parents.Count>0)
                    {
                        string key = c.parents.GetEnumerator().Current.Key;
                        Context parentContextFirst=buildContextForTypeDeclaration(c.parents.GetEnumerator().Current.Value);
                        Context parentContext = parentContextFirst;
                        foreach(var parent in c.parents)
                        {
                            if(parent.Key!=key)
                            {
                                parentContext.parentContext = buildContextForTypeDeclaration(parent.Value);
                                parentContext = parentContext.parentContext;
                            }
                        }
                        newContext.parentContext = parentContextFirst;
                    }
                }
            }else if(typeNode is EnumTypeNode){
                newContext = new Context( typeNode.Identifier.Name, ContextType.ENUM,
                                    getFieldsForEnum(typeNode as EnumTypeNode,path),
                                    null,null);
            }

            newContext.api = this;
            return newContext;
        }

        public Context buildContextForConstructor(KeyValuePair<string, ConstructorNode> ctror)
        {
            Context newContext = new Context(ctror.Key,ContextType.CONSTRUCTOR
                                ,getFixedParameters(ctror.Value.parameters),null,null);
            return newContext;
        }

        private Dictionary<string, FieldNode> getFixedParameters(List<ParameterNode> parameters)
        {
            Dictionary<string, FieldNode> variables = new Dictionary<string, FieldNode>();
            if(parameters!=null)
            foreach (var variable in parameters)
            {
                if(variables.ContainsKey(variable.paramName.Name))
                    Utils.ThrowError("The parameter name '"+variable.paramName.Name+"' is a duplicate ["+currentNamespace.Identifier.Name+"] "+variable.token.getLine());
                variables[variable.paramName.Name] = new FieldNode(variable.paramName,variable.DataType,false,null,null,variable.token);
            }
            return variables;
        }

        private Dictionary<string, FieldNode> getFieldsForEnum(EnumTypeNode enumType, string path)
        {
            Dictionary<string, FieldNode> fields = new Dictionary<string, FieldNode>();
            foreach (var enumeration in enumType.EnumItems)
            {
                fields[enumeration.Identifier.Name] = new FieldNode(enumeration.Identifier,enumType,true
                                ,new EncapsulationNode(TokenType.RW_PUBLIC,null),enumeration.value,enumeration.token);
            }
            return fields;
        }

        public Dictionary<string, FieldNode> getFieldsForClass(ClassTypeNode classType, string path)
        {
            Dictionary<string, FieldNode> my_fields = new Dictionary<string, FieldNode>();
            foreach (var f in classType.Fields)
            {
                if(my_fields.ContainsKey(f.identifier.Name))
                    Utils.ThrowError(path+": The type '"+classType.Identifier.Name
                    +"' already contains a definition for '"+f.identifier.Name+"'");
                my_fields[f.identifier.Name] = f;
            }
            return my_fields;
        }

        public Dictionary<string, ConstructorNode> getConstructorsForClass(ClassTypeNode classType, string path)
        {
            Dictionary<string, ConstructorNode> ctrs = new Dictionary<string, ConstructorNode>();
            foreach (var ct in classType.Constructors)
            {
                string ctrName = ct.identifier.Name + buildFixedParams(ct.parameters);
                if(ctrs.ContainsKey(ctrName))
                    Utils.ThrowError(getDeclarationPathForType(classType)+": Type '"+classType.Identifier.Name
                    +"' already defines a member called '.ctor' with the same parameter types '"+ctrName+"'");
                ctrs[ctrName] = ct;
            }
            if(ctrs.Count==0)
            {
                var token = new Token(TokenType.RW_PUBLIC,"public",0,0);
                ctrs[classType.Identifier.Name+"()"] = new ConstructorNode(classType.Identifier, null,null
                ,new StatementBlockNode(),token);
            }
            return ctrs;
        }

        public Dictionary<string, MethodNode> getMethodsForType(TypeNode typeNode,string path)
        {
            var methodlist = new Dictionary<string,MethodNode>();
            if(typeNode is InterfaceTypeNode)
            {
                var type = typeNode as InterfaceTypeNode;
                foreach (var method in type.methodDeclarationList)
                {
                    var methodPrototype = buildMethodName(method);
                    if(methodlist.ContainsKey(methodPrototype))
                        Utils.ThrowError(getDeclarationPathForType(typeNode)+"duplicated method declaration '"
                        +methodPrototype+"' on: "+typeNode.Identifier.Name);
                    methodlist[methodPrototype] = new MethodNode();
                    methodlist[methodPrototype].methodHeaderNode = method;
                }
            }else if(typeNode is ClassTypeNode)
            {
                var type = typeNode as ClassTypeNode;
                foreach (var method in type.Methods)
                {
                    var methodPrototype = buildMethodName(method.methodHeaderNode);
                    if(methodlist.ContainsKey(methodPrototype))
                        Utils.ThrowError(getDeclarationPathForType(typeNode)+"duplicated method declaration '"
                        +methodPrototype+"' on: "+typeNode.Identifier.Name);
                    methodlist[methodPrototype] = method;
                }
            }else{
                Utils.ThrowError(path+" -> Not a Class or interface type. Methods couldn't be extracted");
            }
            return methodlist;
        }

        public void checkFixedParametersForMethod(MethodHeaderNode methodDe, NamespaceNode currentNamespace)
        {
            TypeNode type = methodDe.returnType.DataType;
            if(methodDe.returnType.DataType is ArrayTypeNode)
            {
                var t = methodDe.returnType.DataType as ArrayTypeNode;
                type = t.DataType;
            }
            TypeNode returnType = getTypeForIdentifier(type.ToString());
            if(returnType==null)
                Utils.ThrowError("The type name '"+methodDe.returnType.DataType.token.lexeme
                +"' could not be found (are you missing a using directive or an assembly reference?) ["+currentNamespace.Identifier.Name+"]("
                +methodDe.returnType.DataType.token.getLine()+")");
            if(methodDe.fixedParams==null)return;
            checkFixedParameters(methodDe.fixedParams,currentNamespace);
        }

        public void checkFixedParameters(List<ParameterNode> parameters, NamespaceNode currentNamespace)
        {
            if(parameters!=null)
            foreach (var fixedParam in parameters)
            {
                var type = fixedParam.DataType;
                if(fixedParam.DataType is ArrayTypeNode)
                {
                    var t = fixedParam.DataType as ArrayTypeNode;
                    type = t.DataType;
                }
                TypeNode fixedType = getTypeForIdentifier(type.ToString());
                if(fixedType==null)
                    Utils.ThrowError("The type name '"+fixedParam.DataType.token.lexeme
                    +"' could not be found (are you missing a using directive or an assembly reference?) ["+currentNamespace.Identifier.Name+"]("
                    +fixedParam.DataType.token.getLine()+")");
            }
        }

        public bool validateModifier(MethodModifierNode modifier, params TokenType[] types)
        {
            if (modifier == null)
                return false;
            foreach (TokenType t in types) {
                if (modifier.token.type == t)
                    return true;
            }
            return false;
        }

        public void checkParentMethodOnMe(MethodNode my_method, MethodNode parent_method,TypeNode child, TypeNode parent,NamespaceNode currentNamespace)
        {
            if(parent is InterfaceTypeNode)
            {
                if(my_method.Modifier!=null)
                {
                    if(!validateModifier(my_method.Modifier,TokenType.RW_VIRTUAL,TokenType.RW_ABSTRACT))
                        Utils.ThrowError("Modifier: "+my_method.Modifier.token.lexeme+" can't be applied to method: "
                        +buildMethodName(my_method.methodHeaderNode)+" ["+currentNamespace.Identifier.Name+"] "+my_method.Modifier.token.getLine());
                }
            }else if(parent is ClassTypeNode)
            {
                var childMethodName = child.Identifier.Name+"."+buildMethodName(my_method.methodHeaderNode);
                var parentMethodName = parent.Identifier.Name+"."+buildMethodName(parent_method.methodHeaderNode);
                if(!validateModifier(my_method.Modifier,TokenType.RW_OVERRIDE))
                    Utils.ThrowError("'"+childMethodName+"' hides inherited member '"+parentMethodName+"'. To make the current member override that "+
                    "implementation, add the override keyword. ["+currentNamespace.Identifier.Name+"] "+my_method.token.getLine());
                if(my_method.statemetBlock==null)
                    Utils.ThrowError("'"+childMethodName+"' must declare a body because it is not marked abstract ["
                    +currentNamespace.Identifier.Name+"] "+my_method.token.getLine());
                if(!validateModifier(parent_method.Modifier,TokenType.RW_VIRTUAL,TokenType.RW_ABSTRACT))
                    Utils.ThrowError("'"+childMethodName+"': cannot override inherited member '"+
                    parentMethodName+"' because it is not marked virtual, abstract. ["+currentNamespace.Identifier.Name+"] "+my_method.token.getLine());
                if(parent_method.encapsulation.type==TokenType.RW_PRIVATE)
                    Utils.ThrowError("'"+childMethodName+"': no suitable method found to override. ["+currentNamespace.Identifier.Name+"] "+ my_method.token.getLine());
                if(!my_method.encapsulation.Equals(parent_method.encapsulation))
                    Utils.ThrowError("'"+childMethodName+"': cannot change access modifiers when overriding '"
                    +parent_method.encapsulation.token.lexeme+"' inherited member '"+parentMethodName+"' ["+currentNamespace.Identifier.Name+"] "+my_method.token.getLine());
            }

            if(!my_method.methodHeaderNode.returnType.Equals(parent_method.methodHeaderNode.returnType))
                Utils.ThrowError("Method: "+buildMethodName(my_method.methodHeaderNode)
                +" hide method "+parent.ToString()+"."+buildMethodName(parent_method.methodHeaderNode)
                +". Not the same return type. ["+currentNamespace.Identifier.Name+"] "+my_method.token.getLine());
            my_method.evaluated = true;
        }

        public void hasCycleInheritance(string name, Dictionary<string, TypeNode> parents)
        {
            if(parents==null)return;
            foreach (var parent in parents)
            {
                hasCycleInheritance(name,parent);
            }
        }

        public void hasCycleInheritance(string name, KeyValuePair<string, TypeNode> parent)
        {
            if(parent.Value is InterfaceTypeNode)
            {
                if(name == parent.Value.Identifier.Name)
                    Utils.ThrowError("Inherited interface '"+parent.Value.Identifier.Name+"' causes a cycle in the interface hierarchy of '"+ name +"'");
                hasCycleInheritance(name,((InterfaceTypeNode)parent.Value).parents);
            }else if(parent.Value is ClassTypeNode)
            {
                if(name == parent.Value.Identifier.Name)
                    Utils.ThrowError("Inherited class '"+parent.Value.Identifier.Name+"' causes a cycle in the class hierarchy of '"+ name +"'");
                // hasCycleInheritance(name,((ClassTypeNode)parent.Value).parents);//TODO
            }
        }

        public string buildMethodName(MethodHeaderNode methodDe)
        {
            var nameDefinition = methodDe.Identifier.Name;
            return nameDefinition + buildFixedParams(methodDe.fixedParams);
        }

        private string buildFixedParams(List<ParameterNode> fixedParams)
        {
            List<string> typesParams = new List<string>();
            if(fixedParams != null)
            {
                foreach (var parameter in fixedParams)
                {
                    typesParams.Add(parameter.DataType.ToString());
                }
            }
            return "(" + string.Join(",",typesParams) + ")";
        }

        public TypeNode getTypeForIdentifier(string name)
        {
            var usingDirectivesList = currentNamespace.usingDirectivesList();
            if(currentNamespace.Identifier.Name!="default")usingDirectivesList.Insert(0,currentNamespace.Identifier.Name);
            foreach (var usd in usingDirectivesList)
            {
                if(Singleton.typesTable.ContainsKey(usd+"."+name))
                    return Singleton.typesTable[usd+"."+name];
            }
            if(Singleton.typesTable.ContainsKey(name))
                return Singleton.typesTable[name];
            return null;
        }

        public NamespaceNode getNamespaceForType(TypeNode typeNode)
        {
            foreach (var tree in trees)
            {
                foreach (var entry in tree.Value.defaultNamespace.typesDeclarations)
                {
                    if(entry == typeNode)
                    {
                        return tree.Value.defaultNamespace;
                    }
                }
                foreach (var ns in tree.Value.namespaceDeclared)
                {
                    foreach (var entry in ns.typesDeclarations)
                    {
                        if(entry == typeNode)
                        {
                            return ns;
                        }
                    }
                }
            }
            return null;
        }

        public string getDeclarationPathForType(TypeNode typeNode)
        {
            string path = "";
            string filePath = "";
            foreach (var tree in trees)
            {
                if(filePath.Length>0)
                    break;
                foreach (var entry in tree.Value.defaultNamespace.typesDeclarations)
                {
                    if(entry == typeNode)
                    {
                        filePath = tree.Value.origin;
                        break;
                    }
                }
                if(filePath.Length>0)
                    break;
                foreach (var ns in tree.Value.namespaceDeclared)
                {
                    foreach (var entry in ns.typesDeclarations)
                    {
                        if(entry == typeNode)
                        {
                            filePath = tree.Value.origin;
                            break;
                        }
                    }
                    if(filePath.Length>0)
                        break;
                }
            }
            return path + filePath + ":" + typeNode.token.getLine();
        }
    }
}