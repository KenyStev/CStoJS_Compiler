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

        private ArgumentNode()
        {
            expression = null;
        }

        public ArgumentNode(ExpressionNode exp)
        {
            this.expression = exp;
        }
    }
}