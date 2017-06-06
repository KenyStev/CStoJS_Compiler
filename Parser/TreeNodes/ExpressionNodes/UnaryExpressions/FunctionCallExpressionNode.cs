using System.Collections.Generic;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class FunctionCallExpressionNode : PrimaryExpressionNode
    {
        public PrimaryExpressionNode identifier;
        public List<ArgumentNode> arguments;

        public FunctionCallExpressionNode(){}

        public FunctionCallExpressionNode(List<ArgumentNode> arguments,Token token) : base(token)
        {
            this.identifier = null;
            this.arguments = arguments;
        }
    }
}