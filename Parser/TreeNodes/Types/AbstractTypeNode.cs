using System;
using System.Xml.Serialization;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Expressions.UnaryExpressions;

namespace Compiler.TreeNodes.Types
{
    public class AbstractTypeNode : TypeNode
    {
        private AbstractTypeNode(){}
        public AbstractTypeNode(IdNode typeName,Token token)
        {
            base.Identifier = typeName;
            this.token = token;
        }

        public override void Evaluate(API api) //TODO
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Identifier.Name;
        }
    }
}