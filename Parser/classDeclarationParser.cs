using System;

namespace Compiler
{
    public partial class Parser
    {
        /*class-declaration: ;
	        | class-modifier "class" identifier inheritance-base class-body optional-body-end */
        private void class_declaration()
        {
            printIfDebug("class_declaration");
            if(pass(TokenType.RW_ABSTRACT))
                consumeToken();
            if(!pass(TokenType.RW_CLASS))
                throwError("group-declaration 'class' expected");
            consumeToken();
            if(!pass(TokenType.ID))
                throwError("identifier expected");
            consumeToken();
            if(pass(TokenType.PUNT_COLON))
                inheritance_base();
            if(!pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
                throwError("'{' expected");
            class_body();
            optional_body_end();
        }

        /*class-body:
	        | '{' optional-class-member-declaration-list '}' */
        private void class_body()
        {
            printIfDebug("class_body");
            if(!pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
                throwError("'{' expected");
            consumeToken();
            optional_class_member_declaration_list();
            if(!pass(TokenType.PUNT_CURLY_BRACKET_CLOSE))
                throwError("'}' expected");
            consumeToken();
        }

        /*optional-class-member-declaration-list:
            | class-member-declaration optional-class-member-declaration-list
            | EPSILON */
        private void optional_class_member_declaration_list()
        {
            printIfDebug("optional_class_member_declaration_list");
            if(pass(encapsulationTypes,optionalModifierTypes,typesOptions,new TokenType[]{TokenType.RW_VOID}))
            {
                class_member_declaration();
                optional_class_member_declaration_list();
            }else{
                //EPSILON
            }
        }

        /*class-member-declaration: 
	        | encapsulation-modifier class-member-declaration-options */
        private void class_member_declaration()
        {
            printIfDebug("class_member_declaration");
            if(pass(encapsulationTypes))
                encapsulation_modifier();
            if(pass(optionalModifierTypes,typesOptions,new TokenType[]{TokenType.RW_VOID}))
            {
                class_member_declaration_options();
            }else{
                throwError(optionalModifierTypes.ToString() + " or "+ typesOptions.ToString() +"or void expected");
            }
        }

        /*
        ; SEMANTIC: void solo puede ir en un method.
        ; SEMANTIC: optional-modifier no puede ir en contructor ni en un field
        ; SEMANTIC: solo un constructor puede llevar : "base" '(' argument-list ')'
        ; SEMANTIC: validar que el constructor no tenga tipo de retorno
        class-member-declaration-options:
            | optional-modifier type-or-void identifier field-or-method-or-constructor */
        private void class_member_declaration_options()
        {
            printIfDebug("class_member_declaration_options");
            if(pass(optionalModifierTypes))
                consumeToken();

            if(pass(typesOptions,new TokenType[]{TokenType.RW_VOID}))
                consumeToken();
            if(pass(TokenType.ID))
                consumeToken();
            
            field_or_method_or_constructor();
        }

        /*field-or-method-or-constructor:
            | field-declaration
            | method-or-constructor-declaration */
        private void field_or_method_or_constructor()
        {
            printIfDebug("field_or_method_or_constructor");
            if(pass(TokenType.PUNT_PAREN_OPEN))
            {
                method_or_constructor_declaration();
            }else{
                field_declaration();
            }
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
            if(pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
            {
                array_initializer();
            }else{
                expression();
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
            if(pass(new TokenType[]{TokenType.PUNT_CURLY_BRACKET_OPEN},expressionOptions))
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
            if(!pass(new TokenType[]{TokenType.PUNT_CURLY_BRACKET_OPEN},expressionOptions))
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

        /*; SEMANTIC: Validar que si la clase es abstract, el metodo no debe llevar cuerpo.
        ; SEMANTIC: solo un constructor puede llevar : "base" '(' argument-list ')'
        method-or-constructor-declaration:
            | '(' fixed-parameters ')' constructor-initializer maybe-empty-block */
        private void method_or_constructor_declaration()
        {
            printIfDebug("method_or_constructor_declaration");
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

        /*maybe-empty-block:
            | '{' optional-statement-list '}'
            | ';' */
        private void maybe_empty_block()
        {
            printIfDebug("maybe_empty_block");
            if(pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
            {
                consumeToken();
                optional_statement_list();
                if(!pass(TokenType.PUNT_CURLY_BRACKET_CLOSE))
                    throwError("'}' expected");
                consumeToken();
            }else if(pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
            {
                consumeToken();
            }else{
                throwError("'{' or ';' expected");
            }
        }

        /*optional-statement-list:
            | statement-list
            | EPSILON */
        private void optional_statement_list()
        {
            // throw new NotImplementedException();
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
            if(pass(expressionOptions))
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
        private void inheritance_base()
        {
            printIfDebug("inheritance_base");
            if(pass(TokenType.PUNT_COLON))
            {
                consumeToken();
                identifiers_list();
            }else{
                //EPSILON
            }
        }
    }
}