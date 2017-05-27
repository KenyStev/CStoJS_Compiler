using System.Xml.Serialization;

namespace Compiler.TreeNodes.Expressions
{
    public class LiteralIntNode : ExpressionNode
    {
        [XmlElement(typeof(int))]
        public int Value;

        private LiteralIntNode(){}
        public LiteralIntNode(int IntValue)
        {
            this.Value = IntValue;
        }
    }
}