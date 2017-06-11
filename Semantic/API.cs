using System;
using System.Collections.Generic;
using Compiler.TreeNodes;
using Compiler.TreeNodes.Types;

namespace Compiler
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

        private string getDeclarationPathForType(TypeNode typeNode)
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