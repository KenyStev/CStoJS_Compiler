using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Compiler.SemanticAPI;
using Compiler.SemanticAPI.ContextUtils;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Expressions.UnaryExpressions
{
    [XmlType("IdNode")]
    public class IdNode : PrimaryExpressionNode
    {
        [XmlElement(typeof(string))]
        public string Name;
        
        [XmlArray("Attributes"),
        XmlArrayItem("Identifier", Type = typeof(IdNode))]
        public List<IdNode> attributes;
        private bool wasFoundStatic;
        private bool isVariable;
        private bool isFirst;

        private IdNode(){}
        public IdNode(string idValue,Token token)
        {
            this.Name = idValue;
            this.attributes = new List<IdNode>();
            this.token = token;
            this.wasFoundStatic=false;
            this.isVariable=false;
            this.isFirst=false;
        }

        public IdNode(string id, List<IdNode> attr,Token token)
        {
            this.Name = id;
            this.attributes = attr;
            this.token = token;
            this.wasFoundStatic=false;
            this.isVariable=false;
            this.isFirst=false;
        }

        public override string ToString()
        {
            return Name;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            if(Name=="c")
                Console.WriteLine();
            TypeNode t = null;
            try{
                if(type==null)
                {
                    this.isFirst=true;
                    FieldNode f = api.contextManager.findVariable(Name);
                    if(f!=null)
                    {
                        t = f.type;
                        api.isNextStaticContext = false;
                        this.isVariable = true;
                        this.wasFoundStatic = f.isStatic;
                        // if(f.isStatic == isStatic)
                        //     Utils.ThrowError("here shoul crash for static");
                    }else{
                        t = api.getTypeForIdentifier(Name);
                        if(t!=null)
                            api.isNextStaticContext=true;
                    }
                }else{
                    this.isFirst=false;
                    bool accept = false;
                    if(!(type is ClassTypeNode))
                    {
                        type = api.getTypeForIdentifier(type.ToString());
                        accept = true;
                    }

                    Context staticContext = api.buildContextForTypeDeclaration(type);
                    FieldNode f = staticContext.findVariable(Name,Utils.privateLevel,Utils.protectedLevel);
                    
                    bool passed = f.isStatic == isStatic;
                    if(accept)
                        passed = true;

                    if(f!=null && passed)
                        t = f.type;
                }

                if(t == null)
                    Utils.ThrowError("Variable '" + Name + "' could not be found in the current context. ");
            }catch(SemanticException ex){
                Utils.ThrowError(ex.Message+token.getLine());
            }
            returnType = t;
            return t;
        }

        public override void GenerateCode(Writer.Writer Writer, API api)
        {
            string fullPath = "";
            if(isFirst)
                fullPath = api.getFullName(returnType);
            
            if(isVariable && wasFoundStatic)
                fullPath += returnType.ToString();
            else if(isVariable && !wasFoundStatic)
                fullPath = "this";

            if(!isVariable)
                fullPath += Name;

            Writer.WriteString(fullPath);
        }
    }
}