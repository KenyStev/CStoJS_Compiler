using System;
using System.Xml.Serialization;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

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

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            return new FloatTypeNode();
        }
    }
}