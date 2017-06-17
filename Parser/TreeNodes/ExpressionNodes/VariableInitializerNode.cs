using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions
{
    public abstract class VariableInitializerNode
    {
        public Token token;

        public VariableInitializerNode(){}
        public VariableInitializerNode(Token token)
        {
            this.token = token;
        }

        public abstract TypeNode EvaluateType(API api, TypeNode type, bool isStatic);
    }
}