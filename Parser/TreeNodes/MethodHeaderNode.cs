using System.Collections.Generic;
using System.Xml.Serialization;
using Compiler.TreeNodes.Statements;
using Compiler.TreeNodes.Expressions.UnaryExpressions;

namespace Compiler.TreeNodes
{
    public class MethodHeaderNode
    {
        [XmlElement(typeof(ReturnTypeNode))]
        public ReturnTypeNode returnType;

        [XmlElement(typeof(IdNode))]
        public IdNode Identifier;

        [XmlArray("Parameters"),
        XmlArrayItem("Param")]
        public List<ParameterNode> fixedParams;
        public Token token;

        private MethodHeaderNode(){}
        public MethodHeaderNode(ReturnTypeNode returnType, IdNode name, List<ParameterNode> fixedParams,Token token)
        {
            this.returnType = returnType;
            this.Identifier = name;
            this.fixedParams = fixedParams;
            this.token = token;
        }
    }
}