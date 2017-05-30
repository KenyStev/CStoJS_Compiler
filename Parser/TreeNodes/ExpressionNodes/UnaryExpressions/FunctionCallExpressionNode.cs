using System.Collections.Generic;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class FunctionCallExpressionNode : PrimaryExpressionNode
    {
        public PrimaryExpressionNode identifier;
        public List<ArgumentNode> arguments;

        public FunctionCallExpressionNode(){}

        public FunctionCallExpressionNode(PrimaryExpressionNode identifier, 
        List<ArgumentNode> arguments,Token token) : base(token)
        {
            this.identifier = identifier;
            this.arguments = arguments;
        }
    }
}