using System.Xml.Serialization;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions.Literals
{
    public class LiteralBoolNode : LiteralNode
    {
        public bool Value;

        private LiteralBoolNode(){}
        public LiteralBoolNode(string IntValue)
        {
            bool.TryParse(IntValue,out this.Value);
        }
    }
}