using Compiler.TreeNodes.Expressions;
using Compiler.TreeNodes.Types;
using Compiler.TreeNodes.Expressions.UnaryExpressions;

namespace Compiler.TreeNodes.Statements
{
    public class ForeachStatementNode : IterationStatementNode
    {
        public TypeNode type;
        public IdNode identifier;
        public ExpressionNode expression;
        public EmbeddedStatementNode body;

        private ForeachStatementNode(){}
        public ForeachStatementNode(TypeNode type, IdNode identifier, ExpressionNode exp, 
        EmbeddedStatementNode body,Token token) : base(token)
        {
            this.type = type;
            this.identifier = identifier;
            this.expression = exp;
            this.body = body;
        }
    }
}