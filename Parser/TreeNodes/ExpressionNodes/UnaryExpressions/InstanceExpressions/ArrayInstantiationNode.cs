using System;
using System.Collections.Generic;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions.InstanceExpressions
{
    public class ArrayInstantiationNode : InstanceExpressionNode
    {
        public TypeNode typeOfArray;
        public List<ExpressionNode> primaryExpBrackets;
        public ArrayTypeNode arrayType;
        public ArrayInitializerNode initialization;

        public ArrayInstantiationNode(){}

        public ArrayInstantiationNode(TypeNode type, List<ExpressionNode> primaryExpBrackets, 
        ArrayTypeNode arrayType, ArrayInitializerNode initialization,Token token) : base(token)
        {
            this.typeOfArray = type;
            this.primaryExpBrackets = primaryExpBrackets;
            this.arrayType = arrayType;
            this.initialization = initialization;
        }

        public ArrayInstantiationNode(TypeNode type, ArrayTypeNode arrayType, 
        ArrayInitializerNode arrayInitializer,Token token) : base(token)
        {
            this.typeOfArray = type;
            this.arrayType = arrayType;
            this.initialization = arrayInitializer;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            var typeOfArrayFinded = api.getTypeForIdentifier(Utils.getNameForType(typeOfArray));
            if(typeOfArrayFinded==null)
                Utils.ThrowError("the type or namespace name '"+
                typeOfArray.ToString()+"' could not be found (are you missing a using directive or an assembly reference?) ["+
                api.currentNamespace.Identifier.Name+"]");
            if(typeOfArrayFinded is VoidTypeNode || typeOfArrayFinded is VarTypeNode)
                Utils.ThrowError("Cannot instaciate array of type 'void' or 'var'. "+token.getLine());
            if(primaryExpBrackets!=null)
            foreach (var exp in primaryExpBrackets)
            {
                var t = exp.EvaluateType(api,null,true);
                if(!(t is IntTypeNode))
                    Utils.ThrowError("Cannot implicitly convert type '"+t.ToString()+"' to 'int' [Parser]");
            }
            if(initialization!=null)
            {
                var tArr = (ArrayTypeNode)initialization.EvaluateType(api,null,true);
                if(!tArr.Equals(arrayType))
                    Utils.ThrowError("Array initiation is invalid! "+tArr.ToString()+" and "
                    +typeOfArray.ToString()+" "+token.getLine());
            }
            return arrayType;
        }
    }
}