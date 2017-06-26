using System;
using System.Collections.Generic;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions.InstanceExpressions
{
    public class ClassInstantioationNode : InstanceExpressionNode
    {
        public TypeNode type;
        public List<ArgumentNode> arguments;

        public ClassInstantioationNode(){}

        public ClassInstantioationNode(TypeNode type, List<ArgumentNode> arguments,Token token) : base(token)
        {
            this.type = type;
            this.arguments = arguments;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            TypeNode t = api.getTypeForIdentifier(this.type.ToString());
            if(t==null)
                Utils.ThrowError("The type or namespace name '"+this.type.ToString()
                +"' could not be found (are you missing a using directive or an assembly reference?) ["
                +api.currentNamespace.Identifier.Name+"]");
            List<string> argumentsTypes = new List<string>();
            foreach (var arg in arguments)
            {
                TypeNode argType = arg.expression.EvaluateType(api,null,true);
                argumentsTypes.Add(argType.ToString());
            }
            if(t.ToString()=="Student")
                Console.Write("");
            String constructorSign = t.ToString() + "(" + string.Join(",",argumentsTypes) + ")";
            var typeContext = api.buildContextForTypeDeclaration(t);
            if(!typeContext.existConstructor(constructorSign))
                Utils.ThrowError("'"+t.ToString()+"' does not contain a constructor that takes "+
                argumentsTypes.Count+" arguments ["+api.currentNamespace.Identifier.Name+"] "+token.getLine());
            return t;
        }
    }
}