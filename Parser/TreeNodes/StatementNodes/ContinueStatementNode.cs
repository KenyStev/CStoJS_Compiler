using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Expressions;

namespace Compiler.TreeNodes.Statements
{
    public class ContinueStatementNode : JumpStatementNode
    {
        public ContinueStatementNode(){}
        public ContinueStatementNode(Token token) : base(token)
        {
        }

        public override void Evaluate(API api)
        {
            if(!api.contextManager.existScopeForContinueAbove())
                Utils.ThrowError("No enclosing loop out of which to break or continue ["
                +api.currentNamespace.Identifier.Name+"] "+token.getLine());
        }
    }
}