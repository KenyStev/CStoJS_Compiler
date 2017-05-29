using System.Xml.Serialization;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions.Literals
{
    public class LiteralStringNode : LiteralNode
    {
        public string Value;

        private LiteralStringNode(){}
        public LiteralStringNode(string IntValue)
        {
            Value = IntValue;
        }
    }
}