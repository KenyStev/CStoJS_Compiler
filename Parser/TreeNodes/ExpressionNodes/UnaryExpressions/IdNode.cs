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

        private IdNode(){}
        public IdNode(string idValue,Token token)
        {
            this.Name = idValue;
            this.attributes = new List<IdNode>();
            this.token = token;
        }

        public IdNode(string id, List<IdNode> attr,Token token)
        {
            this.Name = id;
            this.attributes = attr;
            this.token = token;
        }

        public override string ToString()
        {
            return Name;
        }

        public override TypeNode EvaluateType(API api, TypeNode type, bool isStatic)
        {
            TypeNode t = null;
            if(type==null)
            {
                FieldNode f = api.contextManager.findVariable(Name);
                if(f!=null && f.isStatic == isStatic)
                {
                    t = f.type;
                }else{
                    t = api.getTypeForIdentifier(Name);
                    if(t!=null)
                        isStatic=true;
                }
            }else{
                Context staticContext = api.buildContextForTypeDeclaration(type);
                FieldNode f = staticContext.findVariable(Name,new EncapsulationNode(TokenType.RW_PRIVATE,null));
                if(f!=null && f.isStatic == isStatic)
                    t = f.type;
            }

            if(t == null)
                Utils.ThrowError("Variable '" + Name + "' could not be found in the current context. "+ token.getLine());

            return t;
        }
    }
}