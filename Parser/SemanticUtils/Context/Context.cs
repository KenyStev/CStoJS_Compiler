using System.Collections.Generic;
using Compiler.TreeNodes;

namespace Compiler.SemanticAPI.ContextUtils
{
    public class Context
    {
        public string name;
        public ContextType type;
        public Dictionary<string,FieldNode> variables;
        public Dictionary<string,MethodNode> methods;
        public Dictionary<string,ConstructorNode> constructors;
        public Context parentContext;

        public Context()
        {
            name =  null;
            variables = new Dictionary<string, FieldNode>();
            methods = new Dictionary<string, MethodNode>();
            constructors = new Dictionary<string, ConstructorNode>();
        }

        public Context(string name, ContextType type, Dictionary<string, FieldNode> dictionary1, Dictionary<string, ConstructorNode> dictionary2, Dictionary<string, MethodNode> dictionary3)
        {
            this.name = name;
            this.type = type;
            this.parentContext = null;
            this.variables = dictionary1;
            this.constructors = dictionary2;
            this.methods = dictionary3;
        }

        public override string ToString()
        {
            return name;
        }
    }
}