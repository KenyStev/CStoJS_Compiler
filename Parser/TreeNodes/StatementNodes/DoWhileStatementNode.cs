using System;
using Compiler.SemanticAPI;
using Compiler.SemanticAPI.ContextUtils;
using Compiler.TreeNodes.Expressions;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Statements
{
    public class DoWhileStatementNode : IterationStatementNode
    {
        public ExpressionNode expression;
        public EmbeddedStatementNode body;

        private DoWhileStatementNode(){}
        public DoWhileStatementNode(ExpressionNode exp, EmbeddedStatementNode body,Token token) : base(token)
        {
            this.expression = exp;
            this.body = body;
        }

        public override void Evaluate(API api)
        {
            api.contextManager.pushContext(api.buildContext("do-while:"+token.getLine(),ContextType.ITERATIVE,null));
            if(body!=null)
                body.Evaluate(api);
            var expType = expression.EvaluateType(api,null,true);
            if(!(expType is BoolTypeNode))
                Utils.ThrowError("Cannot implicitly convert type '"+expType.ToString()+"' to 'bool' ["+api.currentNamespace.Identifier.Name+"]");
            api.contextManager.popContext();
        }

        public override void GenerateCode(Writer.Writer Writer, API api) {
            Writer.WriteString("\t\tdo {\n");
            if(this.body != null) {
                this.body.GenerateCode(Writer, api);
            }
            Writer.WriteString("\t\t}");
            Writer.WriteString(" while(");
            this.expression.GenerateCode(Writer, api);
            Writer.WriteString(");\n");
        }
    }
}