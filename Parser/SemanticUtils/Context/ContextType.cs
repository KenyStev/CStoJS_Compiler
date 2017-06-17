using System.Collections.Generic;
using Compiler.TreeNodes;

namespace Compiler.SemanticAPI.ContextUtils
{
    public enum ContextType
    {
        CLASS,
        INTERFACE,
        PARENT,
        CONSTRUCTOR,
        ATTRIBUTE,
        BASE,
        ITERATIVE
    }
}