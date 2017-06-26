using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Compiler.SemanticAPI;
using Compiler.TreeNodes.Types;

namespace Compiler.TreeNodes.Statements
{
    public class LocalVariableDeclarationNode : StatementNode
    {
        [XmlArray("LocalVariables"),
        XmlArrayItem("Variable")]
        public List<FieldNode> localVariables;

        private LocalVariableDeclarationNode(){}

        public LocalVariableDeclarationNode(List<FieldNode> localVariables,Token token) : base(token)
        {
            this.localVariables = localVariables;
        }

        private void checkFields(API api)
        {
            NamespaceNode myNs = api.currentNamespace;
            Dictionary<string,FieldNode> my_fields = api.getFieldsForContext(localVariables);
            foreach (var field in my_fields)
            {
                var f = field.Value;
                if(f.type is VoidTypeNode)
                    Utils.ThrowError("Field cannot have void type ["+myNs.Identifier.Name+"] "+f.type.token.getLine());
                var typeName =  Utils.getNameForType(f.type);
                var typeNode = api.getTypeForIdentifier(typeName);
                if(typeNode==null)
                    Utils.ThrowError("The type or namespace name '"+ typeName 
                    +"' could not be found (are you missing a using directive or an assembly reference?) ["
                    +myNs.Identifier.Name+"]: "+f.type.token.getLine());
                if(typeNode is VoidTypeNode)
                    Utils.ThrowError("The type '" + typeNode.ToString()+ "' is not valid for field " 
                    + field.Value.identifier.Name.ToString() + " in class " + api.contextManager.getCurrentClassContextName()
                    + ". "+ f.type.token.getLine());
                if(typeNode is ClassTypeNode && Utils.isValidEncapsulationForClass(((ClassTypeNode)typeNode).encapsulation,TokenType.RW_PRIVATE))
                    Utils.ThrowError("The type '" + typeName + "' can't be reached due to encapsulation level. "+ f.type.token.getLine());
                
                // if (f.type is ArrayTypeNode)//CHANGE TYPE
                //     ((ArrayTypeNode)f.type).DataType = typeNode;
                // else
                //     f.type = typeNode;
            }
            checkFieldsAssignment(api,my_fields,myNs);
        }

        private void checkFieldsAssignment(API api, Dictionary<string, FieldNode> my_fields, NamespaceNode myNs)
        {
            foreach (var field in my_fields)
            {
                if(field.Value.assigner!=null)
                {
                    if(field.Key == "Age")
                        Console.Write("");
                    var assigner = field.Value.assigner;

                    TypeNode f = field.Value.type;
                    TypeNode typeAssignmentNode = assigner.EvaluateType(api,null,true);

                    api.validateRelationBetween(ref field.Value.type,ref typeAssignmentNode);
                }
            }
        }

        public override void Evaluate(API api)
        {
            checkFields(api);
            foreach (var variable in localVariables)
            {
                api.contextManager.addVariable(variable);
            }
        }

        public override void GenerateCode(Writer.Writer Writer, API api)
        {
            foreach(var field in localVariables){
                var _field = field.identifier.ToString();
                Writer.WriteString($"\t\tlet {_field}");

                if(field.assigner != null)
                {
                    Writer.WriteString(" = ");
                    field.assigner.GenerateCode(Writer, api);
                }

                Writer.WriteString(";\n");
            }
        }
    }
}