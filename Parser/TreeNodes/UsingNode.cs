using System.Xml.Serialization;
using Compiler.TreeNodes.Expressions.UnaryExpressions;

namespace Compiler.TreeNodes
{
    public class UsingNode
    {
        [XmlElement(typeof(IdNode))]
        public IdNode Identifier;

        private UsingNode()
        {
            Identifier = null;
        }
        public UsingNode(IdNode val)
        {
            this.Identifier = val;
        }
    }
}