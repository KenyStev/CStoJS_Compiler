using System.Collections.Generic;
using System.Xml.Serialization;
using Compiler.TreeNodes.Statements;

namespace Compiler.TreeNodes.Types
{
    public class EnumTypeNode : TypeNode
    {
        [XmlElement(typeof(IdNode))]
        public IdNode Identifier;
        
        [XmlArray("Items"),
        XmlArrayItem("EnumItem")]
        public List<EnumNode> EnumItems;
        private EnumTypeNode()
        {
            EnumItems = null;
            Identifier = null;
        }
        public EnumTypeNode(IdNode idnode, List<EnumNode> enumerableList)
        {
            this.Identifier = idnode;
            this.EnumItems = enumerableList;
        }
    }
}