using System;
using System.Xml.Serialization;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

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

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            throw new NotImplementedException();
        }
    }
}