using System;
using System.Collections.Generic;
using Compiler.TreeNodes;

namespace Compiler.SemanticAPI.ContextUtils
{
    public class ContextManager
    {
        public Context currentContext;
        public ContextManager(API api)
        {
            currentContext = api.buildContextForClass(Singleton.getTypeNode("Object"));
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
            while(temp.name != "Object")
                temp = temp.parentContext;
            currentContext = temp;
        }
    }
}