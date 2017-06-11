using System.Collections.Generic;
using System.Xml.Serialization;
using Compiler.TreeNodes.Statements;
using Compiler.TreeNodes.Expressions.UnaryExpressions;
using Compiler.SemanticAPI;
using System;

namespace Compiler.TreeNodes.Types
{
    public class EnumTypeNode : TypeNode
    {
        [XmlElement(typeof(IdNode))]
        public IdNode Identifier;
        
        [XmlArray("Items"),
        XmlArrayItem("EnumItem")]
        public List<EnumNode> EnumItems;
        private EnumTypeNode(){}
        public EnumTypeNode(IdNode idnode, List<EnumNode> enumerableList,Token token)
        {
            this.Identifier = idnode;
            this.EnumItems = enumerableList;
            this.token = token;
        }

        public override void Evaluate(API api)//TODO
        {
            throw new NotImplementedException();
        }
    }
}