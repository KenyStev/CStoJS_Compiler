using Compiler.SemanticAPI;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions.Literals
{
    public abstract class LiteralNode : PrimaryExpressionNode
    {
        public LiteralNode(){}
        public LiteralNode(Token token)
        {
            this.token = token;
        }

        public override void GenerateCode(Writer.Writer Writer, API api)
        {
            Writer.WriteString(token.lexeme);
        }
    }
}