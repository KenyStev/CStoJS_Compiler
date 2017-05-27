using System;
using System.Collections.Generic;
using Compiler.TreeNodes;
using Compiler.TreeNodes.Types;

namespace Compiler
{
    public partial class Parser
    {
        /*class-declaration: ;
	        | class-modifier "class" identifier inheritance-base class-body optional-body-end */
        private ClassTypeNode class_declaration() //TODO
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
            consumeToken();
            if(!pass(TokenType.ID))
                throwError("identifier expected");
            var Identifier = new IdNode(token.lexeme);
            consumeToken();
            var inheritanceses = inheritance_base();
            if(!pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
                throwError("'{' expected");
            var newClassType = class_body(Identifier);
            newClassType.setInheritance(inheritanceses);
            newClassType.setAbstract(isAbstract);
            optional_body_end();
            return newClassType;
        }

        /*class-body:
	        | '{' optional-class-member-declaration-list '}' */
        private ClassTypeNode class_body(IdNode Identifier)
        {
            printIfDebug("class_body");
            if(!pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
                throwError("'{' expected");
            consumeToken();
            var newClassType = new ClassTypeNode(Identifier);
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
                var methodModifier = new MethodModifierNode(optionalModifierToken.type);
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
                    var Identifier = new IdNode(token.lexeme);
                    consumeToken();
                    var methodDeclared = method_declaration(Identifier,type);
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
                        constructor_declaration();
                    }
                }else //Field or Method
                {
                    if(!pass(TokenType.ID))
                        throwError("identifier expected");
                    field_or_method_declaration(ref currentClassType, type, encapsulation,null);
                }
            }else //Contructor
            {
                constructor_declaration();
            }
        }

        private void constructor_declaration()
        {
            printIfDebug("constructor_declaration");
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            consumeToken();
            fixed_parameters();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
            constructor_initializer();
            maybe_empty_block();
        }

        private void field_or_method_declaration(ref ClassTypeNode currentClassType,TypeNode type,EncapsulationNode encapsulation,MethodModifierNode modifier)
        {
            printIfDebug("field_or_method_declaration");
            if(!pass(TokenType.ID))
                throwError("identifier expected");
            var Identifier = new IdNode(token.lexeme);
            consumeToken();
            if(pass(TokenType.PUNT_PAREN_OPEN))
            {
                var newMethodDeclared = method_declaration(Identifier,type);
                newMethodDeclared.setEncapsulation(encapsulation);
                if(modifier!=null)
                    newMethodDeclared.setModifier(modifier);
                currentClassType.addMethod(newMethodDeclared);
            }else{
                field_declaration(); //TODO
            }
        }

        private MethodNode method_declaration(IdNode Identifier, TypeNode type)
        {
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            consumeToken();
            var parameters = fixed_parameters();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
            maybe_empty_block();
            return new MethodNode(new MethodHeaderNode(new ReturnTypeNode(type,type is VoidTypeNode),Identifier,parameters));
        }

        /*field-declaration: 
	        | variable-assigner variable-declarator-list-p ';' */
        private void field_declaration()
        {
            printIfDebug("field_declaration");
            variable_assigner();
            variable_declarator_list_p();
            if(!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                throwError("; expected");
            consumeToken();
        }

        /*variable-declarator-list-p:
            | ',' variable-declarator-list
            | EPSILON */
        private void variable_declarator_list_p()
        {
            printIfDebug("variable_declarator_list_p");
            if(pass(TokenType.PUNT_COMMA))
            {
                consumeToken();
                variable_declarator_list();
            }else{
                //EPSILON
            }
        }

        /*variable-declarator-list:
	        | identifier variable-assigner variable-declarator-list-p */
        private void variable_declarator_list()
        {
            printIfDebug("variable_declarator_list");
            if(!pass(TokenType.ID))
                throwError("identifier expected");
            consumeToken();
            variable_assigner();
            variable_declarator_list_p();
        }

        /*variable-assigner:
            | '=' variable-initializer
            | EPSILON */
        private void variable_assigner()
        {
            printIfDebug("variable_assigner");
            if(pass(TokenType.OP_ASSIGN))
            {
                consumeToken();
                variable_initializer();
            }else{
                //EPSILON
            }
        }

        /*variable-initializer:
            | expression
            | array-initializer */
        private void variable_initializer()
        {
            printIfDebug("variable_initializer");
            if(pass(expressionOptions()))
            {
                expression();
            }else if(pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
            {
                array_initializer();
            }else{
                throwError("expression or array initializer '{'");
            }
        }

        /*array-initializer: 
	        | '{' optional-variable-initializer-list '}' */
        private void array_initializer()
        {
            printIfDebug("array_initializer");
            if(!pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
                throwError("'{' expected");
            consumeToken();
            optional_variable_initializer_list();
            if(!pass(TokenType.PUNT_CURLY_BRACKET_CLOSE))
                throwError("'}' expected");
            consumeToken();
        }

        /*optional-variable-initializer-list:
            | variable-initializer-list
            | EPSILON */
        private void optional_variable_initializer_list()
        {
            printIfDebug("optional_variable_initializer_list");
            if(pass(new TokenType[]{TokenType.PUNT_CURLY_BRACKET_OPEN},expressionOptions()))
            {
                variable_initializer_list();
            }else{
                //EPSILON
            }
        }

        /*variable-initializer-list:
	        | variable-initializer variable-initializer-list-p */
        private void variable_initializer_list()
        {
            printIfDebug("variable_initializer_list");
            if(!pass(new TokenType[]{TokenType.PUNT_CURLY_BRACKET_OPEN},expressionOptions()))
                throwError("expression or variable-initializer expected");
            variable_initializer();
            variable_initializer_list_p();
        }

        /*variable-initializer-list-p:
            | ',' variable-initializer-list
            | EPSILON */
        private void variable_initializer_list_p()
        {
            printIfDebug("variable_initializer_list_p");
            if(pass(TokenType.PUNT_COMMA))
            {
                consumeToken();
                variable_initializer_list();
            }else{
                //EPSILON
            }
        }

        /*constructor-initializer:
            | ':' "base" '(' argument-list ')'
            | EPSILON */
        private void constructor_initializer()
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
                consumeToken();
                argument_list();
                if(!pass(TokenType.PUNT_PAREN_CLOSE))
                    throwError("')' expected");
                consumeToken();
            }else{
                //EPSILON
            }
        }

        /*argument-list:
            | expression argument-list-p
            | EPSILON */
        private void argument_list()
        {
            printIfDebug("argument_list");
            if(pass(expressionOptions()))
            {
                expression();
                argument_list_p();
            }else{
                //EPSILON
            }
        }

        /*argument-list-p:
            | ',' expression argument-list-p
            | EPSILON */
        private void argument_list_p()
        {
            printIfDebug("argument_list_p");
            if(pass(TokenType.PUNT_COMMA))
            {
                consumeToken();
                expression();
                argument_list_p();
            }else{
                //EPSILON
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