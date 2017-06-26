using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions
{
    public class AssignExpressionNode : ExpressionNode
    {
        public ExpressionNode leftExpression;
        public TokenType assignType;
        public ExpressionNode rightExpression;
        public List<string> rules;

        public AssignExpressionNode(){}
        public AssignExpressionNode(ExpressionNode unaryExpression, TokenType assignType, 
        ExpressionNode assignExpression,Token token) : base(token)
        {
            this.leftExpression = unaryExpression;
            this.assignType = assignType;
            this.rightExpression = assignExpression;
            rules = new List<string>();

            setRules();
        }

        private void setRules()
        {
            switch (assignType)
            {
                case TokenType.OP_ASSIGN:
                    rules.Add(Utils.Bool + "," + Utils.Bool);
                    rules.Add(Utils.String + "," + Utils.String);
                    rules.Add(Utils.Float + "," + Utils.Int);
                    rules.Add(Utils.Float + "," + Utils.Float);
                    rules.Add(Utils.Float + "," + Utils.Char);
                    rules.Add(Utils.Char + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Int);
                    rules.Add(Utils.String + "," + Utils.Null);
                    rules.Add(Utils.Class + "," + Utils.Null);
                    rules.Add(Utils.Enum + "," + Utils.Enum);
                    break;
                case TokenType.OP_ASSIGN_SUM:
                    rules.Add(Utils.String + "," + Utils.Int);
                    rules.Add(Utils.String + "," + Utils.Float);
                    rules.Add(Utils.String + "," + Utils.Char);
                    rules.Add(Utils.String + "," + Utils.String);
                    rules.Add(Utils.Float + "," + Utils.Int);
                    rules.Add(Utils.Float + "," + Utils.Float);
                    rules.Add(Utils.Float + "," + Utils.Char);
                    rules.Add(Utils.Char + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Int);
                    break;
                case TokenType.OP_ASSIGN_SUBSTRACT:
                    rules.Add(Utils.Float + "," + Utils.Int);
                    rules.Add(Utils.Float + "," + Utils.Float);
                    rules.Add(Utils.Float + "," + Utils.Char);
                    rules.Add(Utils.Char + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Int);
                    break;
                case TokenType.OP_ASSIGN_MULTIPLICATION:
                    rules.Add(Utils.Float + "," + Utils.Int);
                    rules.Add(Utils.Float + "," + Utils.Float);
                    rules.Add(Utils.Float + "," + Utils.Char);
                    rules.Add(Utils.Char + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Int);
                    break;
                case TokenType.OP_ASSIGN_DIVISION:
                    rules.Add(Utils.Float + "," + Utils.Int);
                    rules.Add(Utils.Float + "," + Utils.Float);
                    rules.Add(Utils.Float + "," + Utils.Char);
                    rules.Add(Utils.Char + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Int);
                    break;
                case TokenType.OP_ASSIGN_MODULO:
                    rules.Add(Utils.Float + "," + Utils.Int);
                    rules.Add(Utils.Float + "," + Utils.Float);
                    rules.Add(Utils.Float + "," + Utils.Char);
                    rules.Add(Utils.Char + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Int);
                    break;
                case TokenType.OP_ASSIGN_SHIFT_LEFT:
                    rules.Add(Utils.Char + "," + Utils.Char);
                    rules.Add(Utils.Char + "," + Utils.Int);
                    rules.Add(Utils.Int + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Int);
                    break;
                case TokenType.OP_ASSIGN_SHIFT_RIGHT:
                    rules.Add(Utils.Char + "," + Utils.Char);
                    rules.Add(Utils.Char + "," + Utils.Int);
                    rules.Add(Utils.Int + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Int);
                    break;
                case TokenType.OP_ASSIGN_BITWISE_AND:
                    rules.Add(Utils.Char + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Int);
                    break;
                case TokenType.OP_ASSIGN_BITWISE_OR:
                    rules.Add(Utils.Char + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Int);
                    break;
                case TokenType.OP_ASSIGN_XOR:
                    rules.Add(Utils.Char + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Char);
                    rules.Add(Utils.Int + "," + Utils.Int);
                    break;
            }
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            TypeNode t1 = leftExpression.EvaluateType(api,null,true);
            TypeNode t2 = rightExpression.EvaluateType(api,null,true);
            t1 = (t1 is AbstractTypeNode || t1 is ArrayTypeNode)?api.getTypeForIdentifier(Utils.getNameForType(t1)):t1;
            t2 = (t2 is AbstractTypeNode || t2 is ArrayTypeNode)?api.getTypeForIdentifier(Utils.getNameForType(t2)):t2;
            string rule = t1.ToString() + "," + t2.ToString();
            string rule2 = t1.getComparativeType() + "," + t2.ToString();
            string rule3 = t1.getComparativeType() + "," + t2.getComparativeType();
            if (!rule.Contains(rule)
                        && !rule.Contains(rule2)
                        && !rule.Contains(rule3)
                        && !t1.Equals(t2))
            {
                if (t1.getComparativeType() == Utils.Class && t2.getComparativeType() == Utils.Class)
                {
                    if (!api.checkRelationBetween(t1, t2))
                        Utils.ThrowError("Not a valid assignment. Trying to assign " + t2.ToString() + " to field with type " + t1.ToString());
                }
                else if ((!(t1.getComparativeType() == Utils.Class || t1.getComparativeType() == Utils.String) && t2 is NullTypeNode))
                {
                    Utils.ThrowError("Not a valid assignment. Trying to assign " + t2.ToString() + " to field with type " + t1.ToString());
                }
                else if (t1.getComparativeType() == Utils.Var)
                {
                    t1 = t2;
                }
                else if (t1.getComparativeType() == Utils.Array && t2.getComparativeType() == Utils.Array)
                {
                    var token = new Token();
                    token.row = -1;
                    token.column = -1;
                    api.checkArrays(t1, t2, token);
                }
                else
                    throw new SemanticException("Not a valid assignment. Trying to assign " + t2.ToString() + " to field with type " + t1.ToString());
            }
            return t1;
        }
    }
}