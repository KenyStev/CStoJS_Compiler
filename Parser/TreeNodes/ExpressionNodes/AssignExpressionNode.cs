using System.Xml.Serialization;

namespace Compiler.TreeNodes.Expressions
{
    public class AssignExpressionNode : ExpressionNode
    {
        public ExpressionNode unaryExpression;
        public TokenType assignType;
        public ExpressionNode assignExpression;

        public AssignExpressionNode(){}
        public AssignExpressionNode(ExpressionNode unaryExpression, TokenType assignType, 
        ExpressionNode assignExpression,Token token) : base(token)
        {
            this.unaryExpression = unaryExpression;
            this.assignType = assignType;
            this.assignExpression = assignExpression;
        }
    }
}