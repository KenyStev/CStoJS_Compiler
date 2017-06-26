using System;
using System.Collections.Generic;
using Compiler.SemanticAPI;
using Compiler.SemanticAPI.ContextUtils;
using Compiler.TreeNodes.Expressions;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Statements
{
    public class ForStatementNode : IterationStatementNode
    {
        public ForInitializerNode Initializer;
        public ExpressionNode expression;
        public List<StatementExpressionNode> postIncrementStmts;
        public EmbeddedStatementNode StatementBlock;

        private ForStatementNode(){}
        public ForStatementNode(ForInitializerNode forInitializer, ExpressionNode exp, 
        List<StatementExpressionNode> postIncrementStmts, EmbeddedStatementNode stmts,Token token) : base(token)
        {
            this.Initializer = forInitializer;
            this.expression = exp;
            this.postIncrementStmts = postIncrementStmts;
            this.StatementBlock = stmts;
        }

        public override void Evaluate(API api)
        {
            api.contextManager.pushContext(api.buildContext("for:"+token.getLine(),ContextType.ITERATIVE,(Initializer.localVariables!=null)?Initializer.localVariables.localVariables:null));
            if(expression!=null)
            {
                var expType = expression.EvaluateType(api,null,true);
                if(!(expType is BoolTypeNode))
                    Utils.ThrowError("Cannot implicitly convert type '"+expType.ToString()+"' to 'bool' ["+api.currentNamespace.Identifier.Name+"]");
            }
            if(StatementBlock!=null)
            StatementBlock.Evaluate(api);
            api.contextManager.popContext();
        }

        public override void GenerateCode(Writer.Writer Writer, API api) {
            Writer.WriteStringLine($"\t\tfor (");
            if(this.Initializer != null) {
                this.Initializer.GenerateCode(Writer, api);
            }
            Writer.WriteString(";");
            if(this.expression != null) {
                expression.GenerateCode(Writer, api);
            }
            Writer.WriteString(";");
            Writer.WriteStringLine(") {");
            if (this.StatementBlock != null) {
                StatementBlock.GenerateCode(Writer, api);
            }
            Writer.WriteStringLine("\t\t}");
        }
    }
}