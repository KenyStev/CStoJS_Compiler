using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Compiler.SemanticAPI;

namespace Compiler.TreeNodes.Types
{
    [XmlType("ArrayType")]
    public class ArrayTypeNode : TypeNode
    {
        [XmlElement(typeof(TypeNode)),
        XmlElement(typeof(PrimitiveTypeNode),ElementName = "PrimitiveTypeNode"),
        XmlElement(typeof(AbstractTypeNode),ElementName = "AbstractTypeNode")]
        public TypeNode DataType;

        [XmlArray("MultiDimArrays"),
        XmlArrayItem("MultiDimArray")]
        public List<MultidimensionArrayTypeNode> multidimsArrays;

        private ArrayTypeNode(){}
        public ArrayTypeNode(TypeNode type, List<MultidimensionArrayTypeNode> multidimsArrays,Token token)
        {
            this.DataType = type;
            this.multidimsArrays = multidimsArrays;
            this.token = token;
            this.Identifier = type.Identifier;
        }

        public override void Evaluate(API api)//TODO
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            List<string> arrayBrackets = new List<string>();
            foreach (var rank in multidimsArrays)
            {
                arrayBrackets.Add(rank.ToString());
            }
            return DataType.ToString()+string.Join(".",arrayBrackets);
        }

        public override bool Equals(object obj)
        {
            if(obj is ArrayTypeNode)
            {
                var o = obj as ArrayTypeNode;
                if(DataType.Equals(o.DataType))
                {
                    for(int i=0; i<multidimsArrays.Count; i++)
                    {
                        if(!multidimsArrays[i].Equals(o.multidimsArrays[i]))
                            return false;
                    }
                    return true;
                }
            }
            return false;
        }

        public override string getComparativeType()
        {
            return Utils.Array;
        }
    }
}