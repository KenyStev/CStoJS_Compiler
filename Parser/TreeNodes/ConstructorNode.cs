using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Compiler.TreeNodes
{
    public class ConstructorNode
    {
        [XmlElement(typeof(EncapsulationNode))]
        public EncapsulationNode encapsulation;

        [XmlElement(typeof(IdNode))]
        public IdNode identifier;

        [XmlArray("Parameters"),
        XmlArrayItem("Param")]
        public List<ParameterNode> parameters;

        [XmlElement(typeof(ConstructorInitializerNode))]
        public ConstructorInitializerNode initializer;

        private ConstructorNode()
        {
            this.identifier = null;
            this.parameters = null;
            this.initializer = null;
            this.encapsulation = null;
        }
        public ConstructorNode(IdNode identifier, List<ParameterNode> parameters, ConstructorInitializerNode initializer)
        {
            this.identifier = identifier;
            this.parameters = parameters;
            this.initializer = initializer;
            this.encapsulation = null;
        }

        public void setEncapsulation(EncapsulationNode encapsulation)
        {
            this.encapsulation = encapsulation;
        }
    }
}