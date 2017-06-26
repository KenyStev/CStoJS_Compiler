using System;
using System.Collections.Generic;
using Compiler.TreeNodes;
using Compiler.TreeNodes.Types;

namespace Compiler.SemanticAPI.ContextUtils
{
    public class ContextManager
    {
        public Context currentContext;
        private API api;

        public ContextManager(API api)
        {
            this.api = api;
            currentContext = getObjectContext();
        }

        public bool existScopeForBreakAbove()
        {
            Context temp = currentContext;
            while(temp.type!=ContextType.CLASS)
            {
                if(temp.type==ContextType.ITERATIVE || temp.type==ContextType.SWITCH)
                    return true;
                temp = temp.parentContext;
            }
            return false;
        }

        public bool existScopeForReturnAbove()
        {
            Context temp = currentContext;
            while(temp.type!=ContextType.CLASS)
            {
                if(temp.type==ContextType.CONSTRUCTOR 
                    || temp.type==ContextType.METHOD
                    || temp.type==ContextType.STATIC_METHOD)
                    return true;
                temp = temp.parentContext;
            }
            return false;
        }

        public string getContextNameFromReturnType()
        {
            Context temp = currentContext;
            while(temp.type!=ContextType.CLASS)
            {
                if(temp.type==ContextType.CONSTRUCTOR 
                    || temp.type==ContextType.METHOD
                    || temp.type==ContextType.STATIC_METHOD)
                    return temp.parentContext.contextName+"."+temp.contextName;
                temp = temp.parentContext;
            }
            return "";
        }

        public TypeNode getReturnType()
        {
            Context temp = currentContext;
            while(temp.type!=ContextType.CLASS)
            {
                if(temp.type==ContextType.CONSTRUCTOR 
                    || temp.type==ContextType.METHOD
                    || temp.type==ContextType.STATIC_METHOD)
                    return temp.returnType;
                temp = temp.parentContext;
            }
            return null;
        }

        public bool existScopeForContinueAbove()
        {
            Context temp = currentContext;
            while(temp.type!=ContextType.CLASS)
            {
                if(temp.type==ContextType.ITERATIVE)
                    return true;
                temp = temp.parentContext;
            }
            return false;
        }

        public void pushContext(Context newCurrent)
        {
            Console.WriteLine("evaluatint: pushContext -> "+newCurrent.contextName);
            if(newCurrent.contextName=="if:line(58,17)")
                Console.Write("");
            var temp = newCurrent;
            while(temp.parentContext!=null)
            {
                temp = temp.parentContext;
            }
            temp.parentContext = currentContext;
            currentContext = newCurrent;
        }

        public TypeNode getTypeFromContext(ContextType contextType)
        {
            Context temp = currentContext;
            while (temp.type!=ContextType.CLASS)
            {
                temp = temp.parentContext;
            }
            temp = (contextType == ContextType.BASE)?temp.parentContext:temp;
            return api.getTypeForIdentifier(temp.contextName);//Singleton.getTypeNode(temp.contextName);
        }

        public void backContextToObject()
        {
            var temp = currentContext;
            while(temp.contextName != "Object")
                temp = temp.parentContext;
            currentContext = temp;
        }

        public MethodNode findFunction(string methodName)
        {
            return currentContext.findFunction(methodName,null);
        }

        public FieldNode findVariable(string name)
        {
            return currentContext.findVariable(name,null);
        }

        public Context getObjectContext()
        {
            return api.buildContextForTypeDeclaration(Singleton.getTypeNode("Object"));
        }

        public void popContext()
        {
            Console.WriteLine("evaluatint: popContext -> "+currentContext.contextName);
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

        public bool existBaseConstructor(List<string> argsTypes)
        {
            string args = "("+string.Join(",",argsTypes)+")";
            return currentContext.parentContext.existConstructor(currentContext.parentContext.contextName+args);
        }
    }
}