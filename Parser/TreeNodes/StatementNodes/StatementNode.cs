using Compiler.SemanticAPI;

namespace Compiler.TreeNodes.Statements
{
    public abstract class StatementNode
    {
        protected Token token;
        public StatementNode(){}
        public StatementNode(Token token)
        {
            this.token = token;
        }

        public abstract void Evaluate(API api);
    }
}