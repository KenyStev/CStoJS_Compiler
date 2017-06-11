using System;
using System.Xml.Serialization;
using Compiler.SemanticAPI;

namespace Compiler.TreeNodes.Types
{
    [XmlType("TypeNode")]
    public abstract class TypeNode
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

        public abstract void Evaluate(API api);
    }
}