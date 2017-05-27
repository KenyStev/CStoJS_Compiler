using System;
using System.Xml.Serialization;

namespace Compiler.TreeNodes
{
    public class MethodNode
    {
        [XmlElement(typeof(EncapsulationNode))]
        public EncapsulationNode encapsulation;

        [XmlElement(typeof(MethodModifierNode))]
        public MethodModifierNode Modifier;

        [XmlElement(typeof(MethodHeaderNode))]
        public MethodHeaderNode methodHeaderNode;
        
        private MethodNode()
        {
            encapsulation = null;
            methodHeaderNode = null;
            Modifier = null;
        }

        public MethodNode(MethodHeaderNode methodHeaderNode)
        {
            this.methodHeaderNode = methodHeaderNode;
            this.encapsulation = new EncapsulationNode(TokenType.RW_PUBLIC);
            this.Modifier = null;
        }

        public void setEncapsulation(EncapsulationNode encapsulation)
        {
            this.encapsulation = encapsulation;
        }

        public void setModifier(MethodModifierNode modifier)
        {
            this.Modifier = modifier;
        }
    }
}