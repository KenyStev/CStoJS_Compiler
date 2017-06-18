using System;
using System.Collections.Generic;
using Compiler.TreeNodes;

namespace Compiler.SemanticAPI.ContextUtils
{
    public class ContextManager
    {
        public Context currentContext;
        private API api;

        public ContextManager(API api)
        {
            this.api = api;
            currentContext = api.buildContextForTypeDeclaration(Singleton.getTypeNode("Object"));
        }

        public void pushContext(Context newCurrent)
        {
            var temp = newCurrent;
            while(temp.parentContext!=null)
            {
                temp = temp.parentContext;
            }
            temp.parentContext = currentContext;
            currentContext = newCurrent;
        }

        public void backContextToObject()
        {
            var temp = currentContext;
            while(temp.contextName != "Object")
                temp = temp.parentContext;
            currentContext = temp;
        }

        public FieldNode findVariable(string name)
        {
            return currentContext.findVariable(name,null);
        }

        public void popContext()
        {
            currentContext = currentContext.parentContext;
        }

        public FieldNode findVariableInCurrentContext(string variableName)
        {
            return currentContext.findVariableJustHere(variableName);
        }

        public string getCurrentClassContextName()
        {
            Context temp = currentContext;
            while(temp.type!=ContextType.CLASS)
                temp = temp.parentContext;
            return temp.contextName;
        }

        public void addVariable(FieldNode variable)
        {
            currentContext.addVariable(variable);
        }
    }
}