using Compiler.TreeNodes.Expressions;
using Compiler.TreeNodes.Types;
using Compiler.TreeNodes.Expressions.UnaryExpressions;
using Compiler.SemanticAPI;
using System;
using Compiler.SemanticAPI.ContextUtils;
using System.Collections.Generic;

namespace Compiler.TreeNodes.Statements
{
    public class ForeachStatementNode : IterationStatementNode
    {
        public TypeNode type;
        public IdNode identifier;
        public ExpressionNode expression;
        public EmbeddedStatementNode body;

        private ForeachStatementNode(){}
        public ForeachStatementNode(TypeNode type, IdNode identifier, ExpressionNode exp, 
        EmbeddedStatementNode body,Token token) : base(token)
        {
            this.type = type;
            this.identifier = identifier;
            this.expression = exp;
            this.body = body;
        }

        public override void Evaluate(API api)
        {
            api.contextManager.pushContext(api.buildContext("foreach:"+token.getLine(),ContextType.ITERATIVE,null));
            var expType = expression.EvaluateType(api,null,true);
            if(!(expType is ArrayTypeNode))
                Utils.ThrowError("foreach statement cannot operate on variables of type '"+expType.ToString()+"' because '"
                +expType.ToString()+"' is not an Array ["+api.currentNamespace.Identifier.Name+"]");
            TypeNode resultType = null;
            var dimsCount = 1;
            if(dimsCount==((ArrayTypeNode)expType).multidimsArrays.Count)
            {
                resultType = ((ArrayTypeNode)expType).DataType;
            }else{
                List<MultidimensionArrayTypeNode> dimension = new List<MultidimensionArrayTypeNode>();
                while (dimsCount<((ArrayTypeNode)expType).multidimsArrays.Count)
                {
                    dimension.Add(new MultidimensionArrayTypeNode(((ArrayTypeNode)expType).multidimsArrays[dimsCount].dimensions,null));
                }
                resultType = new ArrayTypeNode(((ArrayTypeNode)expType).DataType,dimension,null);
            }
            TypeNode decType = null;
            if(type is VarTypeNode)
            {
                decType = resultType;
            }else{
                var f = type;
                var typeAssignmentNode = resultType;

                api.validateRelationBetween(ref f, ref typeAssignmentNode);
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
                decType = type;
            }
            api.contextManager.addVariable(new FieldNode(identifier,decType,false,null,null,type.token));

            if(body!=null)
                body.Evaluate(api);

            api.contextManager.popContext();
        }
    }
}