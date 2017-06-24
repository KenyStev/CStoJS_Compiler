using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.TypeTestingExpressions
{
    public class IsTypeTestNode : TypeTestingExpressionNode
    {
        IsTypeTestNode(){}
        public IsTypeTestNode(ExpressionNode leftExpression, TypeNode type,Token token) : base(leftExpression,type,token)
        {
            
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            TypeNode leftType = leftExpression.EvaluateType(api,type,isStatic);
            if(leftType is PrimitiveTypeNode || toType is PrimitiveTypeNode)
                Utils.ThrowError("Invalid expression 'is' with primitive types. " + token.getLine());
            
            return Singleton.typesTable[Utils.Bool];
        }
    }
}