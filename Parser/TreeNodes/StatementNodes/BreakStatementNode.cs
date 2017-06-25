using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Expressions;

namespace Compiler.TreeNodes.Statements
{
    public class BreakStatementNode : JumpStatementNode
    {
        public BreakStatementNode(){}
        public BreakStatementNode(Token token) : base(token)
        {
        }

        public override void Evaluate(API api)
        {
            if(!api.contextManager.existScopeForBreakAbove())
                Utils.ThrowError("No enclosing loop out of which to break or continue ["
                +api.currentNamespace.Identifier.Name+"] "+token.getLine());
        }
    }
}