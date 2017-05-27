using System.Xml.Serialization;
using Compiler.TreeNodes.Expressions;

namespace Compiler.TreeNodes.Statements
{
    public class AssignNode : StatementNode
    {
        [XmlElement(typeof(IdNode))]
        public IdNode Identifier;

        [XmlElement(typeof(ExpressionNode)),
        XmlElement(typeof(LiteralIntNode))]
        public ExpressionNode expression;
        private AssignNode()
        {
            Identifier = null;
            expression = null;
        }

        public AssignNode(IdNode currentId, LiteralIntNode literalIntNode)
        {
            this.Identifier = currentId;
            this.expression = literalIntNode;
        }
    }
}