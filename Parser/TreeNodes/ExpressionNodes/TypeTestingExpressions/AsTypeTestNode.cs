using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.TypeTestingExpressions
{
    public class AsTypeTestNode : TypeTestingExpressionNode
    {
        AsTypeTestNode(){}
        public AsTypeTestNode(ExpressionNode leftExpression, TypeNode type,Token token) : base(leftExpression, type,token)
        {
            rules.Add($"{Utils.String},{Utils.String}");
            rules.Add($"{Utils.Class},{Utils.Null}");
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            TypeNode leftType = leftExpression.EvaluateType(api,type,isStatic);
            leftType = api.getTypeForIdentifier(Utils.getNameForType(leftType));
            
            if(rules.Contains($"{leftType.getComparativeType()},{toType.ToString()}"))
            {
                return toType;
            }
            TypeNode target = api.getTypeForIdentifier(Utils.getNameForType(toType));
            if(target==null)
                Utils.ThrowError("can not implicitly convert to "+leftType.ToString()+" "+token.getLine());
            if(target is PrimitiveTypeNode || leftType is PrimitiveTypeNode || toType is PrimitiveTypeNode)
                Utils.ThrowError("Cannot use a primity type with this operation. "+token.getLine());
            if(toType is InterfaceTypeNode)
            {
                Utils.ThrowError("Cannot implicity convert type "+leftType.ToString()+ " to interface "+target.ToString());
            }else if(leftType.getComparativeType()==Utils.Interface){
                Utils.ThrowError("Cannot implicity convert type "+leftType.ToString()+ " to interface "+target.ToString());
            }
            target = api.getTypeForIdentifier(toType.ToString());
            if(!api.checkRelationBetween(leftType,target))
                Utils.ThrowError("There is no relation between "+leftType.ToString()+" and "+toType.ToString());
            return target;
        }
    }
}