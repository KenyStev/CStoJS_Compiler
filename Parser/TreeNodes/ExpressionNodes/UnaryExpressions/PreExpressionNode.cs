using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class PreExpressionNode : UnaryExpressionNode
    {
        public TokenType unaryOperator;
        public UnaryExpressionNode unaryExpression;

        public PreExpressionNode(){}

        public PreExpressionNode(TokenType unaryOperator, UnaryExpressionNode unaryExpression,Token token) : base(token)
        {
            this.unaryOperator = unaryOperator;
            this.unaryExpression = unaryExpression;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            TypeNode t1 = unaryExpression.EvaluateType(api,null,true);
            if(api.TokenPass(unaryOperator, TokenType.OP_SUM, TokenType.OP_SUBSTRACT, TokenType.OP_PLUS_PLUS, TokenType.OP_MINUS_MINUS))
            {
                if (t1.ToString() != Utils.Int && t1.ToString() != Utils.Float && t1.ToString() != Utils.Char)
                    throw new SemanticException("Invalid pre unary expression. Cant apply " + unaryOperator.ToString() + " to " + t1.ToString()+" "+ token.getLine());
            }
            if(api.TokenPass(unaryOperator, TokenType.OP_BITWISE_NOT))
            {
                if (t1.ToString() != Utils.Char && t1.ToString() != Utils.Int)
                    throw new SemanticException("Invalid pre unary expression. Cant apply " + unaryOperator.ToString() + " to " + t1.ToString()+" "+token.getLine());
                if (t1.ToString() == Utils.Char)
                    return new IntTypeNode();
            }
            if(api.TokenPass(unaryOperator, TokenType.OP_NOT))
            {
                if(t1.ToString() != Utils.Bool)
                    throw new SemanticException("Invalid pre unary expression. Cant apply " + unaryOperator.ToString() + " to " + t1.ToString()+" "+ token.getLine());
            }
            return t1;
        }
    }
}