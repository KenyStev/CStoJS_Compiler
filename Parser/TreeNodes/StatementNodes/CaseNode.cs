using Compiler.TreeNodes.Expressions;

namespace Compiler.TreeNodes.Statements
{
    public class CaseNode
    {
        public TokenType caseType;
        public ExpressionNode expression;

        private CaseNode(){}
        public CaseNode(TokenType caseType, ExpressionNode exp)
        {
            this.caseType = caseType;
            this.expression = exp;
        }
    }
}