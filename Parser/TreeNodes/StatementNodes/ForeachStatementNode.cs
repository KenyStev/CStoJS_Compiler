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
                decType = type;
            }
            api.contextManager.addVariable(new FieldNode(identifier,decType,false,null,null,type.token));

            if(body!=null)
                body.Evaluate(api);

            api.contextManager.popContext();
        }
    }
}