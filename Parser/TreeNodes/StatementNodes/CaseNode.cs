using Compiler.TreeNodes.Expressions;

namespace Compiler.TreeNodes.Statements
{
    public class CaseNode
    {
        public TokenType caseType;
        public ExpressionNode expression;
        public Token token;

        private CaseNode(){}
        public CaseNode(TokenType caseType, ExpressionNode exp,Token token)
        {
            this.caseType = caseType;
            this.expression = exp;
            this.token = token;
        }
    }
}