using System;
using System.Xml.Serialization;

namespace Compiler.TreeNodes.Types
{
    [XmlType("TypeNode")]
    public class TypeNode
    {
        [XmlElement(typeof(EncapsulationNode))]
        public EncapsulationNode encapsulation;
        public Token token;
        public TypeNode()
        {
            encapsulation = null;
        }
        public void setEncapsulationMode(EncapsulationNode encapMod)
        {
            this.encapsulation = encapMod;
        }
    }
}