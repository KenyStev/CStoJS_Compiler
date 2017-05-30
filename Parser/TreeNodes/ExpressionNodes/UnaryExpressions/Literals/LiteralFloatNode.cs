using System.Xml.Serialization;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions.Literals
{
    public class LiteralFloatNode : LiteralNode
    {
        public float Value;

        private LiteralFloatNode(){}
        public LiteralFloatNode(string IntValue,Token token) : base(token)
        {
            float.TryParse(IntValue,out this.Value);
        }
    }
}