using System;
using System.Collections.Generic;
using Compiler.TreeNodes;
using Compiler.TreeNodes.Expressions.UnaryExpressions;
using Compiler.TreeNodes.Types;

namespace Compiler
{
    public class Singleton
    {
        public static Dictionary<string,List<NamespaceNode>> namespacesTable = new Dictionary<string, List<NamespaceNode>>();
        public static Dictionary<string,TypeNode> typesTable = new Dictionary<string, TypeNode>();
        public static void addNamespace(string name, NamespaceNode ns)
        {

            if(!namespacesTable.ContainsKey(name))
                namespacesTable[name] = new List<NamespaceNode>();    
            namespacesTable[name].Add(ns);
        }

        public static void addType(string name, TypeNode newType)
        {
            typesTable[name] = newType;
        }
    }
}