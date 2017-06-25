using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions
{
    public class TernaryExpressionNode : ExpressionNode
    {
        public ExpressionNode conditionalExpression;
        public ExpressionNode trueExpression;
        public ExpressionNode falseExpression;

        private TernaryExpressionNode(){}
        public TernaryExpressionNode(ExpressionNode conditionalExpression, ExpressionNode trueExpression, 
        ExpressionNode falseExpression,Token token) : base(token)
        {
            this.conditionalExpression = conditionalExpression;
            this.trueExpression = trueExpression;
            this.falseExpression = falseExpression;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            var tBool = conditionalExpression.EvaluateType(api,null,true);
            if(!(tBool is BoolTypeNode))
                Utils.ThrowError("Cannot implicitly convert type '"+tBool.ToString()+"' to 'bool' ["+api.currentNamespace.Identifier.Name+"]");
            var rType = trueExpression.EvaluateType(api,null,true);
            var lType = falseExpression.EvaluateType(api,null,true);
            if(!api.assignmentRules.Contains(rType.ToString()+","+lType.ToString()))
                Utils.ThrowError("Type of conditional expression cannot be determined because there is no implicit conversion between '"+
                rType.ToString()+"' and '"+lType.ToString()+"' ["+api.currentNamespace.Identifier.Name+"]");
            return rType;
        }
    }
}