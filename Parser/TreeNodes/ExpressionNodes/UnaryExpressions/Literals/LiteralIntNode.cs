using System;
using System.Xml.Serialization;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

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

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            throw new NotImplementedException();
        }
    }
}