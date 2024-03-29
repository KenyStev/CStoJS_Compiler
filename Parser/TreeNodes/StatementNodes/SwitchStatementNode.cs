using System;
using Compiler.SemanticAPI;
using Compiler.SemanticAPI.ContextUtils;
using Compiler.TreeNodes.Expressions;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Statements
{
    public class SwitchStatementNode : SelectionStatementNode
    {
        public ExpressionNode expression;
        public SwitchBodyNode switchBodyNode;

        private SwitchStatementNode(){}
        public SwitchStatementNode(ExpressionNode exp, SwitchBodyNode switchBodyNode,Token token) : base(token)
        {
            this.expression = exp;
            this.switchBodyNode = switchBodyNode;
        }

        public override void Evaluate(API api)
        {
            var expType = expression.EvaluateType(api,null,true);
            if(expType is VoidTypeNode && expType is NullTypeNode && !(expType is PrimitiveTypeNode))
                Utils.ThrowError("The switch expression must be a value; found void. ["
                +api.currentNamespace.Identifier.Name+"] "+expression.token.getLine());
            api.contextManager.pushContext(api.buildContext("switch:"+token.getLine(),ContextType.SWITCH,null));
            if(switchBodyNode!=null)
                switchBodyNode.Evaluate(api,expType);
            api.contextManager.popContext();
        }

        public override void GenerateCode(Writer.Writer Writer, API api) {
            Writer.WriteString($"\t\tswitch(");
            this.expression.GenerateCode(Writer, api);
            Writer.WriteString($") {{\n");
            if(this.switchBodyNode != null) {
                switchBodyNode.GenerateCode(Writer, api);
            }
            Writer.WriteString($"\t\t}}\n");
        }
    }
}