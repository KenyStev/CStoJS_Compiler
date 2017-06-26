using System;
using Compiler.SemanticAPI;
using Compiler.SemanticAPI.ContextUtils;
using Compiler.TreeNodes.Expressions;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Statements
{
    public class IfStatementNode : SelectionStatementNode
    {
        public ExpressionNode expression;
        public EmbeddedStatementNode statements;
        public ElseStatementNode elseBock;

        private IfStatementNode(){} 
        public IfStatementNode(ExpressionNode exp, EmbeddedStatementNode stmts, 
        ElseStatementNode elseBock,Token token) : base(token)
        {
            this.expression = exp;
            this.statements = stmts;
            this.elseBock = elseBock;
        }

        public override void Evaluate(API api)
        {
            api.contextManager.pushContext(api.buildContext("if:"+token.getLine(),ContextType.SELECTION,null));
            var expType = expression.EvaluateType(api,null,true);
            if(!(expType is BoolTypeNode || expType.ToString() == "BoolType"))
                Utils.ThrowError("Cannot implicitly convert type '"+expType.ToString()+"' to 'bool' ["+api.currentNamespace.Identifier.Name+"]");
            statements.Evaluate(api);
            api.contextManager.popContext();
            if(elseBock!=null)
                elseBock.Evaluate(api);
        }

        public override void GenerateCode(Writer.Writer Writer, API api)
        {
            Writer.WriteString($"\tif( ");
            this.expression.GenerateCode(Writer, api);
            Writer.WriteString($") {{\n ");
            this.statements.GenerateCode(Writer, api);
            Writer.WriteString($"\n\telse {{\n");
            elseBock.GenerateCode(Writer, api);
            Writer.WriteString($"}}\n ");


        }
    }
}