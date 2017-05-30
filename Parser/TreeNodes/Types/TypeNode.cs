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

        private void setEncapsulationMode(){}
        public void setEncapsulationMode(EncapsulationNode encapMod)
        {
            this.encapsulation = encapMod;
        }
    }
}