using System.Xml.Serialization;
using Compiler.TreeNodes.Expressions;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes
{
    public class FieldNode
    {
        [XmlElement(typeof(IdNode))]
        public IdNode identifier;

        [XmlElement(typeof(TypeNode)),
        XmlElement(typeof(PrimitiveTypeNode)),
        XmlElement(typeof(AbstractTypeNode))]
        public TypeNode type;

        [XmlAttribute(AttributeName = "IsStatic")]
        public bool isStatic;

        [XmlElement(typeof(EncapsulationNode))]
        public EncapsulationNode encapsulation;

        [XmlElement(typeof(VariableInitializerNode)),
        XmlElement(typeof(ArrayInitializerNode)),
        XmlElement(typeof(ExpressionNode))]
        public VariableInitializerNode assigner;

        private FieldNode()
        {
            this.identifier = null;
            this.type = null;
            this.isStatic = false;
            this.encapsulation = null;
            this.assigner = null;
        }

        public FieldNode(IdNode identifier, TypeNode type, bool isStatic, EncapsulationNode encapsulation, VariableInitializerNode assigner)
        {
            this.identifier = identifier;
            this.type = type;
            this.isStatic = isStatic;
            this.encapsulation = encapsulation;
            this.assigner = assigner;
        }
    }
}