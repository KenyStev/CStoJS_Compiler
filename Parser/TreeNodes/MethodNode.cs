using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Compiler.TreeNodes.Statements;
using Compiler.TreeNodes.Expressions.UnaryExpressions;

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

        [XmlElement(typeof(StatementBlockNode))]
        public StatementBlockNode statemetBlock;
        public Token token;

        private MethodNode()
        {
            encapsulation = null;
            methodHeaderNode = null;
            statemetBlock = null;
            Modifier = null;
        }

        public MethodNode(MethodHeaderNode methodHeaderNode,StatementBlockNode statemetBlock,Token token)
        {
            this.methodHeaderNode = methodHeaderNode;
            this.encapsulation = new EncapsulationNode(TokenType.RW_PUBLIC,token);
            this.statemetBlock = statemetBlock;
            this.Modifier = null;
            this.token = token;
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