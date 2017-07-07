using System;
using System.Xml.Serialization;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions.Literals
{
    public class LiteralCharNode : LiteralNode
    {
        public char Value;

        private LiteralCharNode(){}
        public LiteralCharNode(string IntValue,Token token) : base(token)
        {
            char.TryParse(IntValue,out this.Value);
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            return new CharTypeNode();
        }
    }
}