using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

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

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            throw new NotImplementedException();
        }
    }
}