using System;
using System.Collections.Generic;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions
{
    public abstract class BinaryOperatorNode : ExpressionNode
    {
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
            TypeNode leftType = leftOperand.EvaluateType(api,type,isStatic);
            TypeNode rightType = rightOperand.EvaluateType(api,type,isStatic);
            string ruleSign = $"{leftType.getComparativeType()},{rightType.getComparativeType()}";
            if(!rules.ContainsKey(ruleSign))
                Utils.ThrowError($"Operator '{this.token.lexeme}' cannot be applied to operands of type '{leftType.ToString()}' and '{rightType.ToString()}' [{api.currentNamespace.Identifier.Name}] "+token.getLine());
            return rules[ruleSign];
        }
    }
}