using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Compiler.TreeNodes.Statements;

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

        [XmlElement(typeof(StatementNode)),
        XmlElement(typeof(LocalVariableDeclarationNode))]
        public List<StatementNode> statemets;
        
        private MethodNode()
        {
            encapsulation = null;
            methodHeaderNode = null;
            statemets = null;
            Modifier = null;
        }

        public MethodNode(MethodHeaderNode methodHeaderNode,List<StatementNode> statemets)
        {
            this.methodHeaderNode = methodHeaderNode;
            this.encapsulation = new EncapsulationNode(TokenType.RW_PUBLIC);
            this.statemets = statemets;
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