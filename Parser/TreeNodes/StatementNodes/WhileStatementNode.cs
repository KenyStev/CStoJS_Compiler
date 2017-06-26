using System;
using Compiler.SemanticAPI;
using Compiler.SemanticAPI.ContextUtils;
using Compiler.TreeNodes.Expressions;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Statements
{
    public class WhileStatementNode : IterationStatementNode
    {
        public ExpressionNode expression;
        public EmbeddedStatementNode body;

        private WhileStatementNode(){}
        public WhileStatementNode(ExpressionNode exp, EmbeddedStatementNode body,Token token) : base(token)
        {
            this.expression = exp;
            this.body = body;
        }

        public override void Evaluate(API api)
        {
            api.contextManager.pushContext(api.buildContext("while:"+token.getLine(),ContextType.ITERATIVE,null));
            var expType = expression.EvaluateType(api,null,true);
            if(!(expType is BoolTypeNode))
                Utils.ThrowError("Cannot implicitly convert type '"+expType.ToString()+"' to 'bool' ["+api.currentNamespace.Identifier.Name+"]");
            if(body!=null)
                body.Evaluate(api);
            api.contextManager.popContext();
        }
    }
}