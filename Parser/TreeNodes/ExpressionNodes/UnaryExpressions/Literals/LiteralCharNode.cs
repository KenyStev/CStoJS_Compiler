using System.Xml.Serialization;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions.Literals
{
    public class LiteralCharNode : LiteralNode
    {
        public char Value;

        private LiteralCharNode(){}
        public LiteralCharNode(string IntValue)
        {
            char.TryParse(IntValue,out this.Value);
        }
    }
}