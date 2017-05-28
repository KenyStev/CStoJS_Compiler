using System.Collections.Generic;
using System.Xml.Serialization;

namespace Compiler.TreeNodes.Expressions
{
    public class ArrayInitializerNode : VariableInitializerNode
    {
        [XmlArray("ArrayInitializers"),
        XmlArrayItem("VariableInitializer")]
        public List<VariableInitializerNode> arrayInitializers;

        private ArrayInitializerNode()
        {
            arrayInitializers = null;
        }
        public ArrayInitializerNode(List<VariableInitializerNode> arrayInitializers)
        {
            this.arrayInitializers = arrayInitializers;
        }
    }
}