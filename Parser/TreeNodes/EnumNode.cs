using System.Xml.Serialization;
using Compiler.TreeNodes.Expressions;
using Compiler.TreeNodes.Expressions.UnaryExpressions;
using Compiler.TreeNodes.Expressions.UnaryExpressions.Literals;

namespace Compiler.TreeNodes
{
    public class EnumNode
    {
        [XmlElement(typeof(IdNode))]
        public IdNode Name;

        [XmlElement(typeof(ExpressionNode)),
        XmlElement(typeof(LiteralIntNode))]
        public ExpressionNode value;

        private EnumNode()
        {
            Name = null;
            value = null;
        }
        public EnumNode(IdNode name, ExpressionNode value)
        {
            this.Name = name;
            this.value = value;
        }
    }
}