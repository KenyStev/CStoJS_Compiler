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
            if(pass(encapsulationOptions,optionalModifiersOptions,typesOptions,new TokenType[]{TokenType.RW_VOID}))
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
            if(pass(encapsulationOptions))
                encapsulation_modifier();
            if(pass(optionalModifiersOptions,typesOptions,new TokenType[]{TokenType.RW_VOID}))
            {
                class_member_declaration_options();
            }else{
                throwError(optionalModifiersOptions.ToString() + " or "+ typesOptions.ToString() +"or void expected");
            }
        }

        /*
        ; SEMANTIC: validar que el constructor no tenga tipo de retorno
        class-member-declaration-options:
            | optional-modifier type-or-void identifier field-or-method-or-constructor */
        private void class_member_declaration_options()
        {
            printIfDebug("class_member_declaration_options");
            if(pass(optionalModifiersOptions))
            {
                Token oprionalModifierToken = token;
                consumeToken();
                if(!pass(typesOptions,new TokenType[]{TokenType.RW_VOID}))
                    throwError("type-or-void expected");
                type_or_void();
                if(!pass(TokenType.ID))
                    throwError("identifier expected");
                if(oprionalModifierToken.type==TokenType.RW_STATIC)
                {
                    field_or_method_declaration();
                }else{
                    consumeToken();
                    method_declaration();
                }
            }else if(pass(typesOptions,new TokenType[]{TokenType.RW_VOID}))
            {
                Token oldToken = token;
                type_or_void();
                
                if(oldToken.type==TokenType.ID)
                {
                    if(pass(TokenType.ID))
                    {
                        field_or_method_declaration();
                    }else if(pass(TokenType.PUNT_PAREN_OPEN)){
                        constructor_declaration();
                    }
                }else{
                    if(!pass(TokenType.ID))
                        throwError("identifier expected");
                    field_or_method_declaration();
                }
            }else{
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

        private void field_or_method_declaration()
        {
            printIfDebug("field_or_method_declaration");
            if(!pass(TokenType.ID))
                throwError("identifier expected");
            consumeToken();
            if(pass(TokenType.PUNT_PAREN_OPEN))
            {
                method_declaration();
            }else{
                field_declaration();
            }
        }

        private void method_declaration()
        {
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            consumeToken();
            fixed_parameters();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
            maybe_empty_block();
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