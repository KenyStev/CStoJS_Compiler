using System.Collections.Generic;
using System.Xml.Serialization;

namespace Compiler.TreeNodes
{
    [XmlType("IdNode")]
    public class IdNode
    {
        [XmlElement(typeof(string))]
        public string Name;

        [XmlArray("Attributes"),
        XmlArrayItem("Identifier", Type = typeof(IdNode))]
        public List<IdNode> attributes;

        private IdNode()
        {
            Name = null;
            attributes = null;
        }
        public IdNode(string idValue)
        {
            this.Name = idValue;
            this.attributes = new List<IdNode>();
        }

        public IdNode(string id, List<IdNode> attr)
        {
            this.Name = id;
            this.attributes = attr;
        }
    }
}