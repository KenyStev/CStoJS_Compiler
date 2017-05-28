using System.Collections.Generic;
using System.Xml.Serialization;
using Compiler.TreeNodes.Statements;

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

        private MethodHeaderNode()
        {
            returnType = null;
            Identifier = null;
            fixedParams = null;
        }
        public MethodHeaderNode(ReturnTypeNode returnType, IdNode name, List<ParameterNode> fixedParams)
        {
            this.returnType = returnType;
            this.Identifier = name;
            this.fixedParams = fixedParams;
        }
    }
}