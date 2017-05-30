using System;
using System.Collections.Generic;
using Compiler.TreeNodes;
using Compiler.TreeNodes.Expressions;
using Compiler.TreeNodes.Types;
using Compiler.TreeNodes.Expressions.UnaryExpressions;

namespace Compiler
{
    public partial class Parser
    {
        /*class-declaration: ;
	        | class-modifier "class" identifier inheritance-base class-body optional-body-end */
        private ClassTypeNode class_declaration()
        {
            printIfDebug("class_declaration");
            var isAbstract = false;
            if(pass(TokenType.RW_ABSTRACT))
            {
                consumeToken();
                isAbstract = true;
            }
            if(!pass(TokenType.RW_CLASS))
                throwError("group-declaration 'class' expected");
            var classTypeToken = token;
            consumeToken();
            if(!pass(TokenType.ID))
                throwError("identifier expected");
            var Identifier = new IdNode(token.lexeme,token);
            consumeToken();
            var inheritanceses = inheritance_base();
            if(!pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
                throwError("'{' expected");
            var newClassType = class_body(Identifier,classTypeToken);
            newClassType.setInheritance(inheritanceses);
            newClassType.setAbstract(isAbstract);
            optional_body_end();
            return newClassType;
        }

        /*class-body:
	        | '{' optional-class-member-declaration-list '}' */
        private ClassTypeNode class_body(IdNode Identifier,Token classTypeToken)
        {
            printIfDebug("class_body");
            if(!pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
                throwError("'{' expected");
            consumeToken();
            var newClassType = new ClassTypeNode(Identifier,classTypeToken);
            optional_class_member_declaration_list(ref newClassType);
            if(!pass(TokenType.PUNT_CURLY_BRACKET_CLOSE))
                throwError("'}' expected");
            consumeToken();
            return newClassType;
        }

        /*optional-class-member-declaration-list:
            | class-member-declaration optional-class-member-declaration-list
            | EPSILON */
        private void optional_class_member_declaration_list(ref ClassTypeNode currentClassType)
        {
            printIfDebug("optional_class_member_declaration_list");
            if(pass(encapsulationOptions,optionalModifiersOptions,typesOptions,voidOption))
            {
                class_member_declaration(ref currentClassType);
                optional_class_member_declaration_list(ref currentClassType);
            }else{
                //EPSILON
            }
        }

        /*class-member-declaration: 
	        | encapsulation-modifier class-member-declaration-options */
        private void class_member_declaration(ref ClassTypeNode currentClassType)
        {
            printIfDebug("class_member_declaration");
            var encapsulation = encapsulation_modifier();
            if(pass(optionalModifiersOptions,typesOptions,voidOption))
            {
                class_member_declaration_options(ref currentClassType,encapsulation);
            }else{
                throwError(optionalModifiersOptions.ToString() + " or "+ typesOptions.ToString() +"or void expected");
            }
        }

        /*
        ; SEMANTIC: validar que el constructor no tenga tipo de retorno
        class-member-declaration-options:
            | optional-modifier type-or-void identifier field-or-method-or-constructor */
        private void class_member_declaration_options(ref ClassTypeNode currentClassType, EncapsulationNode encapsulation)
        {
            printIfDebug("class_member_declaration_options");
            if(pass(optionalModifiersOptions)) //Field or Method
            {
                Token optionalModifierToken = token;
                var methodModifier = new MethodModifierNode(optionalModifierToken);
                consumeToken();
                if(!pass(typesOptions,voidOption))
                    throwError("type-or-void expected");
                var type = type_or_void();
                if(!pass(TokenType.ID))
                    throwError("identifier expected");
                if(optionalModifierToken.type==TokenType.RW_STATIC) //Field
                {
                    field_or_method_declaration(ref currentClassType,type,encapsulation,methodModifier);
                }else{ //Method
                    var Identifier = new IdNode(token.lexeme,token);
                    var identifierMethodToken = token;
                    consumeToken();
                    var methodDeclared = method_declaration(Identifier,type,identifierMethodToken);
                    methodDeclared.setModifier(methodModifier);
                    methodDeclared.setEncapsulation(encapsulation);
                    currentClassType.addMethod(methodDeclared);
                }
            }else if(pass(typesOptions,voidOption)) //Field, Method or constructor
            {
                Token oldToken = token;
                var type = type_or_void();
                
                if(oldToken.type==TokenType.ID) //Field, Method or constructor
                {
                    if(pass(TokenType.ID)) //Field or Method
                    {
                        field_or_method_declaration(ref currentClassType, type, encapsulation,null);
                    }else if(pass(TokenType.PUNT_PAREN_OPEN)) //Contructor
                    { 
                        var Identifier = new IdNode(oldToken.lexeme,token);
                        var contructoreDeclaration = constructor_declaration(Identifier);
                        contructoreDeclaration.setEncapsulation(encapsulation);
                        currentClassType.addContructor(contructoreDeclaration);
                    }
                }else //Field or Method
                {
                    if(!pass(TokenType.ID))
                        throwError("identifier expected");
                    field_or_method_declaration(ref currentClassType, type, encapsulation,null);
                }
            }
            // else //Contructor
            // {
            //     constructor_declaration();
            // }
        }

        private ConstructorNode constructor_declaration(IdNode identifier)
        {
            printIfDebug("constructor_declaration");
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            var constructorToken = token;
            consumeToken();
            var parameters = fixed_parameters();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
            var initializer = constructor_initializer();
            var stmtsBlock = maybe_empty_block();
            return new ConstructorNode(identifier,parameters,initializer,stmtsBlock,constructorToken);
        }

        private void field_or_method_declaration(ref ClassTypeNode currentClassType,TypeNode type,EncapsulationNode encapsulation,MethodModifierNode modifier)
        {
            printIfDebug("field_or_method_declaration");
            if(!pass(TokenType.ID))
                throwError("identifier expected");
            var Identifier = new IdNode(token.lexeme,token);
            var identifierMethodOrFielToken = token;
            consumeToken();
            if(pass(TokenType.PUNT_PAREN_OPEN))
            {
                var newMethodDeclared = method_declaration(Identifier,type,identifierMethodOrFielToken);
                newMethodDeclared.setEncapsulation(encapsulation);
                if(modifier!=null)
                    newMethodDeclared.setModifier(modifier);
                currentClassType.addMethod(newMethodDeclared);
            }else{
                var isStatic = (modifier!=null)?modifier.token.type==TokenType.RW_STATIC:false;
                var fieldDeclarationList = field_declaration(type,Identifier,encapsulation,isStatic);
                currentClassType.addFields(fieldDeclarationList);
            }
        }

        private MethodNode method_declaration(IdNode Identifier, TypeNode type,Token identifierMethodOrFielToken)
        {
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            var methodHeaderToken = token;
            consumeToken();
            var parameters = fixed_parameters();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
            var statements = maybe_empty_block();
            return new MethodNode(new MethodHeaderNode(new ReturnTypeNode(type,type is VoidTypeNode,type.token),
                                                        Identifier,parameters,methodHeaderToken),statements,identifierMethodOrFielToken);
        }

        /*field-declaration: 
	        | variable-assigner variable-declarator-list-p ';' */
        private List<FieldNode> field_declaration(TypeNode type, IdNode Identifier,EncapsulationNode encapsulation,bool isStatic)
        {
            printIfDebug("field_declaration");
            var assigner = variable_assigner();
            var newField = new FieldNode(Identifier,type,isStatic,encapsulation,assigner,Identifier.token);
            var fields = variable_declarator_list_p(type,encapsulation,isStatic);
            fields.Insert(0,newField);
            if(!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                throwError("; expected");
            consumeToken();
            return fields;
        }

        /*variable-declarator-list-p:
            | ',' variable-declarator-list
            | EPSILON */
        private List<FieldNode> variable_declarator_list_p(TypeNode type, EncapsulationNode encapsulation, bool isStatic)
        {
            printIfDebug("variable_declarator_list_p");
            if(pass(TokenType.PUNT_COMMA))
            {
                consumeToken();
                return variable_declarator_list(type,encapsulation,isStatic);
            }else{
                return new List<FieldNode>();
            }
        }

        /*variable-declarator-list:
	        | identifier variable-assigner variable-declarator-list-p */
        private List<FieldNode> variable_declarator_list(TypeNode type, EncapsulationNode encapsulation, bool isStatic)
        {
            printIfDebug("variable_declarator_list");
            if(!pass(TokenType.ID))
                throwError("identifier expected");
            var identifier = new IdNode(token.lexeme,token);
            consumeToken();
            var assigner = variable_assigner();
            var fields = variable_declarator_list_p(type,encapsulation,isStatic);
            fields.Insert(0,new FieldNode(identifier,type,isStatic,encapsulation,assigner,identifier.token));
            return fields;
        }

        /*variable-assigner:
            | '=' variable-initializer
            | EPSILON */
        private VariableInitializerNode variable_assigner()
        {
            printIfDebug("variable_assigner");
            if(pass(TokenType.OP_ASSIGN))
            {
                consumeToken();
                return variable_initializer();
            }else{
                return null;
            }
        }

        /*variable-initializer:
            | expression
            | array-initializer */
        private VariableInitializerNode variable_initializer()
        {
            printIfDebug("variable_initializer");
            if(pass(expressionOptions()))
            {
                return expression();
            }else if(pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
            {
                return array_initializer();
            }else{
                throwError("expression or array initializer '{'");
            }
            return null;
        }

        /*array-initializer: 
	        | '{' optional-variable-initializer-list '}' */
        private ArrayInitializerNode array_initializer()
        {
            printIfDebug("array_initializer");
            if(!pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
                throwError("'{' expected");
            var arrayInitializerToken = token;
            consumeToken();
            var arrayInitializers = optional_variable_initializer_list();
            if(!pass(TokenType.PUNT_CURLY_BRACKET_CLOSE))
                throwError("'}' expected");
            consumeToken();
            return new ArrayInitializerNode(arrayInitializers,arrayInitializerToken);
        }

        /*optional-variable-initializer-list:
            | variable-initializer-list
            | EPSILON */
        private List<VariableInitializerNode> optional_variable_initializer_list()
        {
            printIfDebug("optional_variable_initializer_list");
            if(pass(new TokenType[]{TokenType.PUNT_CURLY_BRACKET_OPEN},expressionOptions()))
            {
                return variable_initializer_list();
            }else{
                return new List<VariableInitializerNode>();
            }
        }

        /*variable-initializer-list:
	        | variable-initializer variable-initializer-list-p */
        private List<VariableInitializerNode> variable_initializer_list()
        {
            printIfDebug("variable_initializer_list");
            if(!pass(new TokenType[]{TokenType.PUNT_CURLY_BRACKET_OPEN},expressionOptions()))
                throwError("expression or variable-initializer expected");
            var varInitializer = variable_initializer();
            var varInitializerList = variable_initializer_list_p();
            varInitializerList.Insert(0,varInitializer);
            return varInitializerList;
        }

        /*variable-initializer-list-p:
            | ',' variable-initializer-list
            | EPSILON */
        private List<VariableInitializerNode> variable_initializer_list_p()
        {
            printIfDebug("variable_initializer_list_p");
            if(pass(TokenType.PUNT_COMMA))
            {
                consumeToken();
                return variable_initializer_list();
            }else{
                return new List<VariableInitializerNode>();
            }
        }

        /*constructor-initializer:
            | ':' "base" '(' argument-list ')'
            | EPSILON */
        private ConstructorInitializerNode constructor_initializer()
        {
            printIfDebug("constructor_initializer");
            if(pass(TokenType.PUNT_COLON))
            {
                consumeToken();
                if(!pass(TokenType.RW_BASE))
                    throwError("'base' reserver word expected");
                consumeToken();
                if(!pass(TokenType.PUNT_PAREN_OPEN))
                    throwError("'(' expected");
                var argumentsToken = token;
                consumeToken();
                var arguments = argument_list();
                if(!pass(TokenType.PUNT_PAREN_CLOSE))
                    throwError("')' expected");
                consumeToken();
                return new ConstructorInitializerNode(arguments,argumentsToken);
            }else{
                return null;
            }
        }

        /*argument-list:
            | expression argument-list-p
            | EPSILON */
        private List<ArgumentNode> argument_list()
        {
            printIfDebug("argument_list");
            if(pass(expressionOptions()))
            {
                var expToken = token;
                var exp = expression();
                var argumentList = argument_list_p();
                argumentList.Insert(0,new ArgumentNode(exp,expToken));
                return argumentList;
            }else{
                return new List<ArgumentNode>();
            }
        }

        /*argument-list-p:
            | ',' expression argument-list-p
            | EPSILON */
        private List<ArgumentNode> argument_list_p()
        {
            printIfDebug("argument_list_p");
            if(pass(TokenType.PUNT_COMMA))
            {
                var expToken = token;
                consumeToken();
                var exp = expression();
                var argumentList = argument_list_p();
                argumentList.Insert(0,new ArgumentNode(exp,expToken));
                return argumentList;
            }else{
                return new List<ArgumentNode>();
            }
        }

        /*inheritance-base:
            | ':' identifiers-list
            | EPSILON */
        private List<IdNode> inheritance_base()
        {
            printIfDebug("inheritance_base");
            if(pass(TokenType.PUNT_COLON))
            {
                consumeToken();
                return identifiers_list();
            }else{
                return new List<IdNode>();
            }
        }
    }
}