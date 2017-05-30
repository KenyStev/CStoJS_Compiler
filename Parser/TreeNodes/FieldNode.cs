using System.Xml.Serialization;
using Compiler.TreeNodes.Expressions;
using Compiler.TreeNodes.Types;
using Compiler.TreeNodes.Expressions.UnaryExpressions;

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
        public Token token;

        private FieldNode(){}

        public FieldNode(IdNode identifier, TypeNode type, bool isStatic, EncapsulationNode encapsulation, 
        VariableInitializerNode assigner,Token token)
        {
            this.identifier = identifier;
            this.type = type;
            this.isStatic = isStatic;
            this.encapsulation = encapsulation;
            this.assigner = assigner;
            this.token = token;
        }
    }
}