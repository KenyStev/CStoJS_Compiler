using System;
using System.Xml.Serialization;

namespace Compiler.TreeNodes.Types
{
    [XmlType("TypeNode")]
    public class TypeNode
    {
        [XmlElement(typeof(EncapsulationNode))]
        public EncapsulationNode encapsulation;

        private void setEncapsulationMode()
        {
            encapsulation = null;
        }
        public void setEncapsulationMode(EncapsulationNode encapMod)
        {
            this.encapsulation = encapMod;
        }
    }
}