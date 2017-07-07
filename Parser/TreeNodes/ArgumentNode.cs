using System.Xml.Serialization;
using Compiler.TreeNodes.Expressions;
using Compiler.TreeNodes.Expressions.UnaryExpressions.Literals;

namespace Compiler.TreeNodes
{
    public class ArgumentNode
    {
        [XmlElement(typeof(ExpressionNode)),
        XmlElement(typeof(LiteralIntNode))]
        public ExpressionNode expression;
        public Token token;

        private ArgumentNode(){}

        public ArgumentNode(ExpressionNode exp,Token token)
        {
            this.expression = exp;
            this.token = token;
        }
    }
}