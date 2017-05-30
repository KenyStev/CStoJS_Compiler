using System.Xml.Serialization;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions.Literals
{
    public class LiteralIntNode : LiteralNode
    {
        public int Value;

        private LiteralIntNode(){}
        public LiteralIntNode(string IntValue,Token token) : base(token)
        {
            int.TryParse(IntValue,out this.Value);
        }
        public LiteralIntNode(int IntValue,Token token) : base(token)
        {
            this.Value = IntValue;
        }
    }
}