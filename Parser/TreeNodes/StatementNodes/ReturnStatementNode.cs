using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Expressions;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Statements
{
    public class ReturnStatementNode : JumpStatementNode
    {
        public ExpressionNode expression;

        public ReturnStatementNode(){}
        public ReturnStatementNode(ExpressionNode exp, Token token) : base(token)
        {
            this.expression = exp;
        }

        public override void Evaluate(API api)
        {
            if(!api.contextManager.existScopeForReturnAbove())
                Utils.ThrowError("Cannot return here "+token.getLine());
            TypeNode expVal = expression.EvaluateType(api,null,true);
            TypeNode retType = api.contextManager.getReturnType();
            string retTypeContextName = api.contextManager.getContextNameFromReturnType();

            if(retType==null && expVal!=null)
                Utils.ThrowError("Since '"+retTypeContextName+"' returns void, a return keyword must not be followed by an object expression ["+
                api.currentNamespace.Identifier.Name+"]");
            
            var f = retType;
            var typeAssignmentNode = expVal;

            api.validateRelationBetween(ref f,ref typeAssignmentNode);

            // var tdn = typeAssignmentNode;
            // var t = (f is AbstractTypeNode)?api.getTypeForIdentifier(Utils.getNameForType(f)):f;
            // t = (t is VarTypeNode)?tdn:t;
            // string rule = f.ToString() + "," + typeAssignmentNode.ToString();
            // string rule2 = (t is AbstractTypeNode)?"":t.getComparativeType() + "," + typeAssignmentNode.ToString();
            // string rule3 = (t is AbstractTypeNode)?"":t.getComparativeType() + "," + typeAssignmentNode.getComparativeType();
            //     if (!api.assignmentRules.Contains(rule)
            //     && !api.assignmentRules.Contains(rule2)
            //     && !api.assignmentRules.Contains(rule3)
            //     && f.ToString() != typeAssignmentNode.ToString()
            //     && !f.Equals(typeAssignmentNode))
            // {
            //     f = (f is AbstractTypeNode)?api.getTypeForIdentifier(Utils.getNameForType(f)):f;
            //     f = (f is VarTypeNode)?tdn:f;
            //     if(f.getComparativeType() == Utils.Class && tdn.getComparativeType() == Utils.Class)
            //     {
            //         if(!api.checkRelationBetween(f, tdn))
            //             Utils.ThrowError("1Not a valid assignment. Trying to assign " + tdn.ToString() + " to field with type " + f.ToString()+" "+ token.getLine());
            //     }else if ((!(f.getComparativeType() == Utils.Class || f.getComparativeType() == Utils.String) && tdn is NullTypeNode))
            //     {
            //         Utils.ThrowError("2Not a valid assignment. Trying to assign " + tdn.ToString() + " to field with type " + f.ToString()+" "+ token.getLine());
            //     }
            //     else
            //         Utils.ThrowError("3Not a valid assignment. Trying to assign " + tdn.ToString() + " to field with type " + f.ToString()+" "+ token.getLine());
            // }
        }
    }
}