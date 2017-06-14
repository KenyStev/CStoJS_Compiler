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

        public void checkFixedParameters(MethodHeaderNode methodDe, NamespaceNode myNs)
        {
            TypeNode returnType = getTypeForIdentifier(methodDe.returnType.DataType.ToString(),myNs.usingDirectivesList(),myNs);
            if(returnType==null)
                Utils.ThrowError("The type name '"+methodDe.returnType.DataType.token.lexeme
                +"' could not be found (are you missing a using directive or an assembly reference?) ["+myNs.Identifier.Name+"]("
                +methodDe.returnType.token.getLine()+")");
            if(methodDe.fixedParams==null)return;
            foreach (var fixedParam in methodDe.fixedParams)
            {
                TypeNode fixedType = getTypeForIdentifier(fixedParam.DataType.ToString(),myNs.usingDirectivesList(),myNs);
                if(fixedType==null)
                    Utils.ThrowError("The type name '"+fixedParam.DataType.token.lexeme
                    +"' could not be found (are you missing a using directive or an assembly reference?) ["+myNs.Identifier.Name+"]("
                    +fixedParam.token.getLine()+")");
            }
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