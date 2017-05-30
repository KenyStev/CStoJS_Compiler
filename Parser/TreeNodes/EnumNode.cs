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
        public Token token;

        private EnumNode(){}
        public EnumNode(IdNode name, ExpressionNode value,Token token)
        {
            this.Name = name;
            this.value = value;
            this.token = token;
        }
    }
}