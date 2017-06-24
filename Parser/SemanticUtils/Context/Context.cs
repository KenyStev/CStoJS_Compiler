using System;
using System.Collections.Generic;
using Compiler.TreeNodes;

namespace Compiler.SemanticAPI.ContextUtils
{
    public class Context
    {
        public string contextName;
        public ContextType type;
        public Dictionary<string,FieldNode> variables;
        public Dictionary<string,MethodNode> methods;
        public Dictionary<string,ConstructorNode> constructors;
        public Context parentContext;
        public API api;

        public Context()
        {
            contextName =  null;
            variables = new Dictionary<string, FieldNode>();
            methods = new Dictionary<string, MethodNode>();
            constructors = new Dictionary<string, ConstructorNode>();
        }

        public Context(string name, ContextType type, Dictionary<string, FieldNode> dictionary1, Dictionary<string, ConstructorNode> dictionary2, Dictionary<string, MethodNode> dictionary3)
        {
            this.contextName = name;
            this.type = type;
            this.parentContext = null;
            this.variables = dictionary1;
            this.constructors = dictionary2;
            this.methods = dictionary3;
        }

        public FieldNode findVariable(string variableName,params EncapsulationNode[] notToBeEncapsulation)
        {
            if(variables.ContainsKey(variableName))
            {
                var variable =  variables[variableName];
                if(notToBeEncapsulation==null) return variable;
                else
                {
                    foreach (var enc in notToBeEncapsulation)
                    {
                        if(enc.Equals(variable.encapsulation))
                            Utils.ThrowError(""+contextName+"."+variable.identifier.Name
                            +"' is inaccessible due to its protection level ["
                            +api.currentNamespace.Identifier.Name+"]");
                    }
                }
                return variable;
            }
            else if (parentContext.contextName!="Object")
                return parentContext.findVariable(variableName,Utils.privateLevel);
            return null;
        }

        public bool existConstructor(string constructorSign)
        {
            if(constructors!=null && constructors.ContainsKey(constructorSign))
                return true;
            return false;
        }

        public void addVariable(FieldNode variable)
        {
            if(variables.ContainsKey(variable.identifier.Name))
                Utils.ThrowError("A local variable or function named '"+variable.identifier.Name
                    +"' is already defined in this scope ["+api.currentNamespace.Identifier.Name+"]");
            variables[variable.identifier.Name] = variable;
        }

        public FieldNode findVariableJustHere(string variableName)
        {
            FieldNode f = null;
            if(variables.ContainsKey(variableName))
                f = variables[variableName];
            return f;
        }

        public override string ToString()
        {
            return contextName;
        }
    }
}