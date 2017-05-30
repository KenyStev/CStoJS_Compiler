using System.Collections.Generic;
using System.Xml.Serialization;

namespace Compiler.TreeNodes.Expressions
{
    public class ArrayInitializerNode : VariableInitializerNode
    {
        [XmlArray("ArrayInitializers"),
        XmlArrayItem("VariableInitializer")]
        public List<VariableInitializerNode> arrayInitializers;

        private ArrayInitializerNode(){}
        public ArrayInitializerNode(List<VariableInitializerNode> arrayInitializers,Token token) : base(token)
        {
            this.arrayInitializers = arrayInitializers;
        }
    }
}