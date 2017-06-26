using System;
using System.Collections.Generic;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions
{
    public abstract class BinaryOperatorNode : ExpressionNode
    {
        string leftComparativeType,rightComparativeType;
        public ExpressionNode leftOperand;
        public ExpressionNode rightOperand;
        public Dictionary<string,TypeNode> rules;

        public BinaryOperatorNode(){} 
        public BinaryOperatorNode(ExpressionNode leftExpression, ExpressionNode relationalExpression,
        Token token) : base(token)
        {
            this.leftOperand = leftExpression;
            this.rightOperand = relationalExpression;
            this.token = token;
            rules = new Dictionary<string, TypeNode>();
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            TypeNode leftType = leftOperand.EvaluateType(api,null,isStatic);
            leftType = api.getTypeForIdentifier(Utils.getNameForType(leftType));
            TypeNode rightType = rightOperand.EvaluateType(api,null,isStatic);
            string ruleSign = $"{leftType.getComparativeType()},{rightType.getComparativeType()}";
            if(!rules.ContainsKey(ruleSign))
                Utils.ThrowError($"Operator '{this.token.lexeme}' cannot be applied to operands of type '{leftType.ToString()}' and '{rightType.ToString()}' [{api.currentNamespace.Identifier.Name}] "+token.getLine());
            returnType = rules[ruleSign];
            leftComparativeType = leftType.getComparativeType();
            rightComparativeType = rightType.getComparativeType();
            return returnType;
        }

        public override void GenerateCode(Writer.Writer Writer, API api)
        {
            if(!Utils.passAssignExpression(token))
                Writer.WriteString("(");

            if(returnType is IntTypeNode)
                Writer.WriteString("ToIntPrecision(");

            if (leftComparativeType == "CharType" && rightComparativeType == "IntType")
            {
                Writer.WriteString("CharToInt(");
                leftOperand.GenerateCode(Writer, api);
                Writer.WriteString(")");
                Writer.WriteString($" {token.lexeme} ");
                Writer.WriteString("ToIntPrecision(");
                rightOperand.GenerateCode(Writer, api);
                Writer.WriteString(")");
            }
            else if (rightComparativeType == "CharType" && leftComparativeType == "IntType")
            {
                Writer.WriteString("ToIntPrecision(");
                leftOperand.GenerateCode(Writer, api);
                Writer.WriteString(")");
                Writer.WriteString($" {token.lexeme} ");
                Writer.WriteString("CharToInt(");
                rightOperand.GenerateCode(Writer, api);
                Writer.WriteString(")");
            }
            else if (leftComparativeType == "CharType" && rightComparativeType == "CharType")
            {
                Writer.WriteString("CharToInt(");
                leftOperand.GenerateCode(Writer, api);
                Writer.WriteString(")");
                Writer.WriteString($" {token.lexeme} ");
                Writer.WriteString("CharToInt(");
                rightOperand.GenerateCode(Writer, api);
                Writer.WriteString(")");
            }

            if(returnType is IntTypeNode)
                Writer.WriteString(")");
            if(!Utils.passAssignExpression(token))
                Writer.WriteString(")");
        }
    }
}