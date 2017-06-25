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
            if(identifier.ToString()=="n7")
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
                            isStatic = false;
                    }else{
                        arrayType = api.getTypeForIdentifier(identifier.ToString());
                        if(arrayType!=null)
                            isStatic=true;
                    }
                }else{
                    Context staticContext = api.buildContextForTypeDeclaration(type);
                    FieldNode f = staticContext.findVariable(identifier.ToString(),Utils.privateLevel,Utils.protectedLevel);
                    if(f!=null && f.isStatic == isStatic)
                        arrayType = f.type;
                }

                if(arrayType == null)
                    Utils.ThrowError("Array variable '" + identifier.ToString() + "' could not be found in the current context. "+ token.getLine());
            }catch(Exception ex){
                Utils.ThrowError(ex.Message+token.getLine());
            }
            return arrayType;
        }
    }
}