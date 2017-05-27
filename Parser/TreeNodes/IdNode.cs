using System.Collections.Generic;
using System.Xml.Serialization;

namespace Compiler.TreeNodes
{
    public class IdNode
    {
        [XmlElement(typeof(string))]
        public string Identifier;

        [XmlArray("Attributes"),
        XmlArrayItem("Identifier")]
        public List<IdNode> attributes;

        private IdNode()
        {
            Identifier = null;
            attributes = null;
        }
        public IdNode(string idValue)
        {
            this.Identifier = idValue;
            this.attributes = new List<IdNode>();
        }

        public IdNode(string id, List<IdNode> attr)
        {
            this.Identifier = id;
            this.attributes = attr;
        }
    }
}