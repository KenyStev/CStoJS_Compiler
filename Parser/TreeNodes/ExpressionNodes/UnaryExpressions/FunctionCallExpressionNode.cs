using System;
using System.Collections.Generic;
using Compiler;
using Compiler.SemanticAPI;
using Compiler.SemanticAPI.ContextUtils;
using Compiler.TreeNodes;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    public class FunctionCallExpressionNode : PrimaryExpressionNode
    {
        public PrimaryExpressionNode identifier;
        public List<ArgumentNode> arguments;

        public FunctionCallExpressionNode(){}

        public FunctionCallExpressionNode(List<ArgumentNode> arguments,Token token) : base(token)
        {
            this.identifier = null;
            this.arguments = arguments;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            if(identifier.ToString()=="ToString")
                Console.Write("");
            TypeNode t = null;
            try{
                if(type==null)
                {
                    List<string> argumentsTypes = new List<string>();
                    foreach (var arg in arguments)
                    {
                        TypeNode argType = arg.expression.EvaluateType(api,null,true);
                        argumentsTypes.Add(argType.ToString());
                    }

                    string methodName = identifier.ToString()+"(" + string.Join(",",argumentsTypes) + ")";
                    MethodNode f = api.contextManager.findFunction(methodName);
                    if(f!=null)
                    {
                        t = f.methodHeaderNode.returnType.DataType;
                        if(api.validateModifier(f.Modifier,TokenType.RW_STATIC) == isStatic)
                            isStatic = false;
                    }else{
                        t = api.getTypeForIdentifier(identifier.ToString());
                        if(t!=null)
                            isStatic=true;
                    }
                }else{
                    Context staticContext = api.buildContextForTypeDeclaration(type);
                    staticContext.setLastParent(api.contextManager.getObjectContext());
                    List<string> argumentsTypes = new List<string>();
                    foreach (var arg in arguments)
                    {
                        TypeNode argType = arg.expression.EvaluateType(api,null,true);
                        argumentsTypes.Add(argType.ToString());
                    }

                    string methodName = identifier.ToString()+"(" + string.Join(",",argumentsTypes) + ")";
                    MethodNode f = staticContext.findFunction(methodName,Utils.privateLevel,Utils.protectedLevel);
                    if(f!=null && api.validateModifier(f.Modifier,TokenType.RW_STATIC) == isStatic)
                        t = f.methodHeaderNode.returnType.DataType;
                }

                if(t == null)
                    Utils.ThrowError("Function or method '" + identifier.ToString() + "' could not be found in the current context. ");
            }catch(Exception ex){
                Utils.ThrowError(ex.Message+token.getLine());
            }
            return t;
        }
    }
}