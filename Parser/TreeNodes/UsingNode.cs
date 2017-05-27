using System.Xml.Serialization;

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