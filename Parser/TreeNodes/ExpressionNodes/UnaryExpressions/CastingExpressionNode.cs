using System;
using System.Collections.Generic;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class CastingExpressionNode : UnaryExpressionNode
    {
        public TypeNode targetCastType;
        public UnaryExpressionNode expresion;
        List<string> rules;

        public CastingExpressionNode(){}

        public CastingExpressionNode(TypeNode targetCastType, UnaryExpressionNode exp,Token token) : base(token)
        {
            this.targetCastType = targetCastType;
            this.expresion = exp;
            rules = new List<string>();
            rules.Add(Utils.Int + "," + Utils.Int);
            rules.Add(Utils.Int + "," + Utils.Float);
            rules.Add(Utils.Float + "," + Utils.Int);
            rules.Add(Utils.Int + "," + Utils.Char);
            rules.Add(Utils.Char + "," + Utils.Int);

            rules.Add(Utils.Char + "," + Utils.Char);
            rules.Add(Utils.Char + "," + Utils.Float);

            rules.Add(Utils.Float + "," + Utils.Float);
            rules.Add(Utils.Float + "," + Utils.Char);

            rules.Add(Utils.String + "," + Utils.String);
            rules.Add(Utils.Class + "," + Utils.Null);
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            TypeNode expType = expresion.EvaluateType(api,null,isStatic);
            TypeNode t = api.getTypeForIdentifier(Utils.getNameForType(targetCastType));
            string rule = t.ToString() + "," + expType.ToString();
            string rule2 = t.getComparativeType() + "," + expType.ToString();
            string rule3 = t.getComparativeType() + "," + expType.getComparativeType();
            if (rules.Contains(rule) || rules.Contains(rule2) || rules.Contains(rule3) || t.Equals(expType))
            {
                return t;
            }
            if(!(t is ClassTypeNode))
                Utils.ThrowError("There is no relation between "+expType.ToString()+" and "+t.ToString());
            if (expType is ClassTypeNode)
            {
                if (api.checkRelationBetween(expType, t))
                {
                    return t;
                }
            }
            if(!(expType is NullTypeNode))
                Utils.ThrowError("There is no relation between "+expType.ToString()+" and "+t.ToString());
                
            return t;
        }
    }
}