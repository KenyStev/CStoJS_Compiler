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

        public override void Evaluate(API api)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Identifier.Name;
        }

        public string getDefinitionType()
        {
            return "AbstractType";
        }

        public override bool Equals(object obj)
        {
            if(obj is AbstractTypeNode)
            {
                var o = obj as AbstractTypeNode;
                string oName = (o.Identifier.attributes!=null)?o.Identifier.attributes[o.Identifier.attributes.Count-1].Name:o.Identifier.Name;
                return oName == Identifier.Name;
            }
            return false;
        }

        public override string getComparativeType()
        {
            throw new NotImplementedException();
        }
    }
}