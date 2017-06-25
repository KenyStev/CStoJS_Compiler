using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class PostAdditiveExpressionNode : PrimaryExpressionNode
    {
        public PrimaryExpressionNode primary;
        public TokenType opType;

        public PostAdditiveExpressionNode(){}

        public PostAdditiveExpressionNode(TokenType type,Token token) : base(token)
        {
            this.primary = null;
            this.opType = type;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            TypeNode tdn = primary.EvaluateType(api,null,true);
            if (tdn.getComparativeType() != Utils.Int && tdn.getComparativeType() != Utils.Float && tdn.getComparativeType() != Utils.Char)
                Utils.ThrowError("Invalid operation. Cant make post unary expression of a type '" + tdn.ToString() + "'. "+ token.getLine());
            return tdn;
        }
    }
}