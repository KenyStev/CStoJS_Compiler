using System.Xml.Serialization;
using Compiler.TreeNodes.Expressions.UnaryExpressions;

namespace Compiler.TreeNodes
{
    public class UsingNode
    {
        [XmlElement(typeof(IdNode))]
        public IdNode Identifier;
        public Token token;

        private UsingNode(){}
        public UsingNode(IdNode val,Token usingToken)
        {
            this.Identifier = val;
            this.token = usingToken;
        }
    }
}