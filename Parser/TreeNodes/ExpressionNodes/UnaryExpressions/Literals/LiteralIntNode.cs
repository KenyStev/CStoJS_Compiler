using System.Xml.Serialization;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions.Literals
{
    public class LiteralIntNode : LiteralNode
    {
        public int Value;

        private LiteralIntNode(){}
        public LiteralIntNode(string IntValue)
        {
            int.TryParse(IntValue,out this.Value);
        }
        public LiteralIntNode(int IntValue)
        {
            this.Value = IntValue;
        }
    }
}