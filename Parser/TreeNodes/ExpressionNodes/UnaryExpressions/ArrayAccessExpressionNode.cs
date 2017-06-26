using System;
using System.Collections.Generic;
using Compiler.SemanticAPI;
using Compiler.SemanticAPI.ContextUtils;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class ArrayAccessExpressionNode : PrimaryExpressionNode
    {
        public PrimaryExpressionNode identifier;
        public List<List<ExpressionNode>> arrayAccessList;

        public ArrayAccessExpressionNode(){}

        public ArrayAccessExpressionNode(List<List<ExpressionNode>> arrayAccessList,Token token) : base(token)
        {
            this.identifier = null;
            this.arrayAccessList = arrayAccessList;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            if(identifier.ToString()=="students")
                Console.Write("");
            TypeNode arrayType = null;
            try{
                if(type==null)
                {
                    FieldNode f = api.contextManager.findVariable(identifier.ToString());
                    if(f!=null)
                    {
                        arrayType = f.type;
                        if(f.isStatic == isStatic)
                            api.isNextStaticContext = false;
                    }else{
                        arrayType = api.getTypeForIdentifier(identifier.ToString());
                        if(arrayType!=null)
                            api.isNextStaticContext=true;
                    }
                }else{
                    bool accept = false;
                    if(!(type is ClassTypeNode))
                    {
                        type = api.getTypeForIdentifier(type.ToString());
                        accept = true;
                    }

                    Context staticContext = api.buildContextForTypeDeclaration(type);
                    FieldNode f = staticContext.findVariable(identifier.ToString(),Utils.privateLevel,Utils.protectedLevel);
                    bool passed = f.isStatic == isStatic;
                    if(accept)
                        passed = true;

                    if(f!=null && passed)
                        arrayType = f.type;
                }

                if(arrayType == null)
                    Utils.ThrowError("Array variable '" + identifier.ToString() + "' could not be found in the current context. ");

                var arr = arrayType as ArrayTypeNode;
                int arraysOfArraysCounter = 0;

                while (arraysOfArraysCounter < arrayAccessList.Count)
                {
                    if(arraysOfArraysCounter>arr.multidimsArrays.Count)
                        Utils.ThrowError("Cannot apply indexing with [] to an expression of type '"+arr.DataType.ToString()
                        +"' ["+api.currentNamespace.Identifier.Name+"]");
                    if(arr.multidimsArrays[arraysOfArraysCounter].dimensions!=arrayAccessList[arraysOfArraysCounter].Count)
                        Utils.ThrowError("Wrong number of indices inside []; expected "+
                        arr.multidimsArrays[arraysOfArraysCounter].dimensions+" ["+api.currentNamespace.Identifier.Name+"]");
                    arraysOfArraysCounter++;
                }

                if(arraysOfArraysCounter==arr.multidimsArrays.Count)
                {
                    arrayType = arr.DataType;
                }else{
                    var dimensions = new List<MultidimensionArrayTypeNode>();
                    while (arraysOfArraysCounter<arr.multidimsArrays.Count)
                    {
                        dimensions.Add(new MultidimensionArrayTypeNode(arr.multidimsArrays[arraysOfArraysCounter].dimensions,null));
                        arraysOfArraysCounter++;
                    }
                    arrayType = new ArrayTypeNode(arr.DataType,dimensions,null);
                }

            }catch(SemanticException ex){
                Utils.ThrowError(ex.Message+token.getLine());
            }
            return arrayType;
        }
    }
}