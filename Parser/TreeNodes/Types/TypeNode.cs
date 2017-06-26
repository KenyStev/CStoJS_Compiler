using System;
using System.Xml.Serialization;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Expressions.UnaryExpressions;

namespace Compiler.TreeNodes.Types
{
    [XmlType("TypeNode")]
    public abstract class TypeNode
    {
        public IdNode Identifier;
        public EncapsulationNode encapsulation;
        public Token token;
        public bool evaluated;
        public bool generated;
        public TypeNode()
        {
            encapsulation = null;
        }
        public void setEncapsulationMode(EncapsulationNode encapMod)
        {
            this.encapsulation = encapMod;
        }

        public abstract void Evaluate(API api);
        public virtual void GenerateCode(Writer.Writer Writer) {
            Console.WriteLine($"Generating code for {this.Identifier}");
        }
        public abstract string getComparativeType();
    }
}