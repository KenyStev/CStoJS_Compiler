using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Expressions;

namespace Compiler.TreeNodes.Statements
{
    public class StatementExpressionNode : EmbeddedStatementNode
    {
        public ExpressionNode expressionNode;

        public StatementExpressionNode(){}
        public StatementExpressionNode(ExpressionNode expressionNode,Token token) : base(token)
        {
            this.expressionNode = expressionNode;
        }

        public override void Evaluate(API api)
        {
            expressionNode.EvaluateType(api,null,true);
        }
    }
}