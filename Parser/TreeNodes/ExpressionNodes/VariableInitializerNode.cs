using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions
{
    public abstract class VariableInitializerNode
    {
        public Token token;
        public TypeNode returnType=null;

        public VariableInitializerNode(){}
        public VariableInitializerNode(Token token)
        {
            this.token = token;
        }

        public abstract TypeNode EvaluateType(API api, TypeNode type, bool isStatic);

        public virtual void GenerateCode(Writer.Writer Writer, API api) {
            Writer.WriteString($"{token.lexeme}");
        }
    }
}