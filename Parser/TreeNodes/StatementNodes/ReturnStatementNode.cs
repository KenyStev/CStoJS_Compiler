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
        }
        
        public override void GenerateCode(Writer.Writer Writer, API api) {
            Writer.WriteString("\t\treturn");
            this.expression.GenerateCode(Writer, api);
            Writer.WriteString(";");
        }
    }
}