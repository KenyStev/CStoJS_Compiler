using System.Collections.Generic;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions.InstanceExpressions
{
    public class ArrayInstantiationNode : InstanceExpressionNode
    {
        public TypeNode type;
        public List<ExpressionNode> primaryExpBrackets;
        public ArrayTypeNode arrayType;
        public ArrayInitializerNode initialization;

        public ArrayInstantiationNode(){}

        public ArrayInstantiationNode(TypeNode type, List<ExpressionNode> primaryExpBrackets, 
        ArrayTypeNode arrayType, ArrayInitializerNode initialization,Token token) : base(token)
        {
            this.type = type;
            this.primaryExpBrackets = primaryExpBrackets;
            this.arrayType = arrayType;
            this.initialization = initialization;
        }

        public ArrayInstantiationNode(TypeNode type, ArrayTypeNode arrayType, 
        ArrayInitializerNode arrayInitializer,Token token) : base(token)
        {
            this.type = type;
            this.arrayType = arrayType;
            this.initialization = arrayInitializer;
        }
    }
}