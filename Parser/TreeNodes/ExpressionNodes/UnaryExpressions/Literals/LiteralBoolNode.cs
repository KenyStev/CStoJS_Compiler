using System.Xml.Serialization;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions.Literals
{
    public class LiteralBoolNode : LiteralNode
    {
        public bool Value;

        private LiteralBoolNode(){}
        public LiteralBoolNode(string IntValue,Token token) : base(token)
        {
            bool.TryParse(IntValue,out this.Value);
        }
    }
}