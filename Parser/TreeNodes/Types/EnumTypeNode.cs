using System.Collections.Generic;
using System.Xml.Serialization;
using Compiler.TreeNodes.Statements;
using Compiler.TreeNodes.Expressions.UnaryExpressions;
using Compiler.SemanticAPI;
using System;
using Compiler.TreeNodes.Expressions.UnaryExpressions.Literals;

namespace Compiler.TreeNodes.Types
{
    public class EnumTypeNode : TypeNode
    {   
        [XmlArray("Items"),
        XmlArrayItem("EnumItem")]
        public List<EnumNode> EnumItems;
        private EnumTypeNode(){}
        public EnumTypeNode(IdNode idnode, List<EnumNode> enumerableList,Token token)
        {
            this.Identifier = idnode;
            this.EnumItems = enumerableList;
            this.token = token;
        }

        public override void Evaluate(API api)
        {
            var thisDeclarationPath = api.getDeclarationPathForType(this);
            if(!Utils.isValidEncapsulation(this.encapsulation,TokenType.RW_PUBLIC))
                Utils.ThrowError("Elements defined in a namespace cannot be declared as private or protected; Enum: "
                +this.Identifier.Name+" ("+thisDeclarationPath+") ");
            Dictionary<string,EnumNode> enums = new Dictionary<string,EnumNode>();
            foreach (var item in this.EnumItems)
            {
                if(enums.ContainsKey(item.Identifier.Name))
                    Utils.ThrowError("Enum: ("+thisDeclarationPath+") already contains a item named: "+item.Identifier.Name
                    +" defined before here: "+enums[item.Identifier.Name].token.getLine());
                enums[item.Identifier.Name] = item;
                if(!(enums[item.Identifier.Name].value is LiteralIntNode))
                    Utils.ThrowError("The value for ("+thisDeclarationPath+")["+ this.Identifier.Name+"."
                    +item.Identifier.Name +"]"+item.token.getLine()+" Enum Assignment should be constant-Literal-Int");
            }
        }

        public override string ToString()
        {
            // return Identifier.Name;
            return "EnumType";
        }

        public override bool Equals(object obj)
        {
            if(obj is EnumTypeNode)
            {
                var o = obj as EnumTypeNode;
                return Identifier.Name == o.Identifier.Name;
            }
            return false;
        }
    }
}