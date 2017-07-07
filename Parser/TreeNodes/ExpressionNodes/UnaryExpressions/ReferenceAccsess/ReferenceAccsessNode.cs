using System;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions.ReferenceAccsess
{
    public abstract class ReferenceAccsessNode : PrimaryExpressionNode
    {

        public ReferenceAccsessNode(){}
        public ReferenceAccsessNode(Token token)
        {
            this.token = token;
        }
    }
}