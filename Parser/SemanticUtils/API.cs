using System;
using System.Collections.Generic;
using Compiler.TreeNodes;
using Compiler.TreeNodes.Types;

namespace Compiler.SemanticAPI
{
    public class API
    {
        private Dictionary<string, CompilationUnitNode> trees;

        public API(Dictionary<string, CompilationUnitNode> trees)
        {
            this.trees = trees;
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

        public void checkFixedParameters(MethodHeaderNode methodDe, NamespaceNode myNs)
        {
            TypeNode type = methodDe.returnType.DataType;
            if(methodDe.returnType.DataType is ArrayTypeNode)
            {
                var t = methodDe.returnType.DataType as ArrayTypeNode;
                type = t.DataType;
            }
            TypeNode returnType = getTypeForIdentifier(type.ToString(),myNs.usingDirectivesList(),myNs);
            if(returnType==null)
                Utils.ThrowError("The type name '"+methodDe.returnType.DataType.token.lexeme
                +"' could not be found (are you missing a using directive or an assembly reference?) ["+myNs.Identifier.Name+"]("
                +methodDe.returnType.DataType.token.getLine()+")");
            if(methodDe.fixedParams==null)return;
            foreach (var fixedParam in methodDe.fixedParams)
            {
                type = fixedParam.DataType;
                if(fixedParam.DataType is ArrayTypeNode)
                {
                    var t = fixedParam.DataType as ArrayTypeNode;
                    type = t.DataType;
                }
                TypeNode fixedType = getTypeForIdentifier(type.ToString(),myNs.usingDirectivesList(),myNs);
                if(fixedType==null)
                    Utils.ThrowError("The type name '"+fixedParam.DataType.token.lexeme
                    +"' could not be found (are you missing a using directive or an assembly reference?) ["+myNs.Identifier.Name+"]("
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

        public void checkParentMethodOnMe(MethodNode my_method, MethodNode parent_method,TypeNode child, TypeNode parent,NamespaceNode myNs)
        {
            if(parent is InterfaceTypeNode)
            {
                if(my_method.Modifier!=null)
                {
                    if(!validateModifier(my_method.Modifier,TokenType.RW_VIRTUAL,TokenType.RW_ABSTRACT))
                        Utils.ThrowError("Modifier: "+my_method.Modifier.token.lexeme+" can't be applied to method: "
                        +buildMethodName(my_method.methodHeaderNode)+" ["+myNs.Identifier.Name+"] "+my_method.Modifier.token.getLine());
                }
            }else if(parent is ClassTypeNode)
            {
                var childMethodName = child.Identifier.Name+"."+buildMethodName(my_method.methodHeaderNode);
                var parentMethodName = parent.Identifier.Name+"."+buildMethodName(parent_method.methodHeaderNode);
                if(!validateModifier(my_method.Modifier,TokenType.RW_OVERRIDE))
                    Utils.ThrowError("'"+childMethodName+"' hides inherited member '"+parentMethodName+"'. To make the current member override that "+
                    "implementation, add the override keyword. ["+myNs.Identifier.Name+"] "+my_method.token.getLine());
                if(my_method.statemetBlock==null)
                    Utils.ThrowError("'"+childMethodName+"' must declare a body because it is not marked abstract ["
                    +myNs.Identifier.Name+"] "+my_method.token.getLine());
                if(!validateModifier(parent_method.Modifier,TokenType.RW_VIRTUAL,TokenType.RW_ABSTRACT))
                    Utils.ThrowError("'"+childMethodName+"': cannot override inherited member '"+
                    parentMethodName+"' because it is not marked virtual, abstract. ["+myNs.Identifier.Name+"] "+my_method.token.getLine());
                if(parent_method.encapsulation.type==TokenType.RW_PRIVATE)
                    Utils.ThrowError("'"+childMethodName+"': no suitable method found to override. ["+myNs.Identifier.Name+"] "+ my_method.token.getLine());
                if(!my_method.encapsulation.Equals(parent_method.encapsulation))
                    Utils.ThrowError("'"+childMethodName+"': cannot change access modifiers when overriding '"
                    +parent_method.encapsulation.token.lexeme+"' inherited member '"+parentMethodName+"' ["+myNs.Identifier.Name+"] "+my_method.token.getLine());
            }

            if(!my_method.methodHeaderNode.returnType.Equals(parent_method.methodHeaderNode.returnType))
                Utils.ThrowError("Method: "+buildMethodName(my_method.methodHeaderNode)
                +" hide method "+parent.ToString()+"."+buildMethodName(parent_method.methodHeaderNode)
                +". Not the same return type. ["+myNs.Identifier.Name+"] "+my_method.token.getLine());
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
            var nameDefinition = methodDe.Identifier.Name + "(";
            List<string> typesParams = new List<string>();
            if(methodDe.fixedParams != null)
            {
                foreach (var parameter in methodDe.fixedParams)
                {
                    typesParams.Add(parameter.DataType.ToString());
                }
            }
            return nameDefinition + string.Join(",",typesParams) + ")";
        }

        public TypeNode getTypeForIdentifier(string name, List<string> usingDirectivesList,NamespaceNode myNs)
        {
            if(myNs.Identifier.Name!="default")usingDirectivesList.Insert(0,myNs.Identifier.Name);
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