using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Expressions.UnaryExpressions;
using Compiler.TreeNodes.Expressions.UnaryExpressions.InstanceExpressions;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions
{
    public class ArrayInitializerNode : VariableInitializerNode
    {
        [XmlArray("ArrayInitializers"),
        XmlArrayItem("VariableInitializerNode")]
        public List<VariableInitializerNode> arrayInitializers;

        private ArrayInitializerNode(){}
        public ArrayInitializerNode(List<VariableInitializerNode> arrayInitializers,Token token) : base(token)
        {
            this.arrayInitializers = arrayInitializers;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            TypeNode tdn = null;
            int arraysOfarrays = 0;
            VariableInitializerNode previous_expr = null;
            foreach (VariableInitializerNode vi in arrayInitializers)
            {
                if (previous_expr != null)
                {
                    validateExpressions(previous_expr, vi);
                }
                previous_expr = vi;
                TypeNode tdn2 = vi.EvaluateType(api,null,isStatic);
                if (tdn != null)
                {
                    if (!tdn.Equals(tdn2))
                    {
                        if ((tdn is NullTypeNode && tdn2 is PrimitiveTypeNode) || (tdn2 is NullTypeNode && tdn is PrimitiveTypeNode))
                            throw new SemanticException("Cannot use 'null' and primitive values in an array initialization.");
                        if (!(tdn is NullTypeNode && tdn2 is NullTypeNode))
                            throw new SemanticException("Values in array initialization are not equal. '" + tdn.ToString() + "' and '" + tdn2.ToString() + "'");
                    }

                }
                tdn = tdn2;
                if ((vi is InlineExpressionNode))
                {
                    if (((InlineExpressionNode)vi).primary is ArrayInstantiationNode)
                        arraysOfarrays++;
                }
            }
            if(tdn is ArrayTypeNode)
            {
                if (arraysOfarrays > 0)
                {
                    for (int i = 0; i < arraysOfarrays-1; i++)
                    {
                        ((ArrayTypeNode)tdn).multidimsArrays.Add(new MultidimensionArrayTypeNode(1,null));
                    }
                }
                else
                    ((ArrayTypeNode)tdn).multidimsArrays[0].dimensions++;
            }
            else
            {
                var i = new MultidimensionArrayTypeNode(1,null);
                var l = new List<MultidimensionArrayTypeNode>();
                l.Add(i);
                var a = new ArrayTypeNode(tdn,l,null);
                return a;
            }
            return tdn;
        }

        private void validateExpressions(VariableInitializerNode previous_expr, VariableInitializerNode vi)
        {
            if (previous_expr is ArrayInitializerNode && vi is ArrayInitializerNode)
                return;
            if ((previous_expr is InlineExpressionNode && vi is ArrayInitializerNode) || (vi is InlineExpressionNode && previous_expr is ArrayInitializerNode))
                throw new SemanticException("Invalid array initialization. Cannot interpolate expressions. InlineExpression and ArrayInitializer.");
            ExpressionNode ex1 = ((InlineExpressionNode)previous_expr).primary;
            ExpressionNode ex2 = ((InlineExpressionNode)vi).primary;
            if((ex1 is ArrayInstantiationNode && !(ex2 is ArrayInstantiationNode)) || (ex2 is ArrayInstantiationNode && !(ex1 is ArrayInstantiationNode)))
                throw new SemanticException("Invalid array initialization. Cannot interpolate expressions with ArrayInstantiation.");
        }
    }
}