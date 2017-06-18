using System;
using System.Xml.Serialization;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions.Literals
{
    public class LiteralStringNode : LiteralNode
    {
        public string Value;

        private LiteralStringNode(){}
        public LiteralStringNode(string IntValue,Token token) : base(token)
        {
            Value = IntValue;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            return new StringTypeNode();
        }
    }
}