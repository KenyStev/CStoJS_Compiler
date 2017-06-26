using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Expressions;
using Compiler.TreeNodes.Types;

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

        public void Evaluate(API api, TypeNode expType)
        {
            if(caseType==TokenType.RW_CASE)
            {
                var expCaseType = expression.EvaluateType(api,null,true);
                if(!(expCaseType is PrimitiveTypeNode))
                    Utils.ThrowError("A constant value is expected ["+api.currentNamespace.Identifier.Name+"]");
                api.validateRelationBetween(ref expType,ref expCaseType);
            }
        }
    }
}