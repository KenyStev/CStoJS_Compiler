using System;
using System.Linq;
using Compiler.TreeNodes;

namespace Compiler
{
    public partial class Parser
    {
        /*interface-declaration: 
	        | "interface" identifier inheritance-base interface-body optional-body-end */
        private InterfaceTypeNode interface_declaration() //TODO
        {
            printIfDebug("interface_declaration");
            if(!pass(TokenType.RW_INTERFACE))
                throwError("'interface' expected");
            consumeToken();
            if(!pass(TokenType.ID))
                throwError("identifier expected");
            consumeToken();
            inheritance_base();
            interface_body();
            optional_body_end();
            return null;
        }

        /*interface-body:
	        | '{' interface-method-declaration-list '}' */
        private void interface_body()
        {
            printIfDebug("interface_body");
            if(!pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
                throwError("'{' expected");
            consumeToken();
            interface_method_declaration_list();
            if(!pass(TokenType.PUNT_CURLY_BRACKET_CLOSE))
                throwError("'}' expected");
            consumeToken();
        }

        /*interface-method-declaration-list:
            | interface-method-header ';' interface-method-declaration-list
            | EPSILON */
        private void interface_method_declaration_list()
        {
            printIfDebug("interface_method_declaration_list");
            if(pass(typesOptions,voidOption))
            {
                interface_method_header();
                if(!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                    throwError("; expected");
                consumeToken();
                interface_method_declaration_list();
            }else{
                //EPSILON
            }
        }

        /*interface-method-header:
	        | type-or-void identifier '(' fixed-parameters ')'  */
        private void interface_method_header()
        {
            printIfDebug("interface_method_header");
            if(!pass(typesOptions,voidOption))
                throwError("type-or-void expected");
            type_or_void();
            if(!pass(TokenType.ID))
                throwError("identifier expected");
            consumeToken();
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            consumeToken();
            if(pass(typesOptions))
                fixed_parameters();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
        }

        /*fixed-parameters:
            | fixed-parameter fixed-paramaters-p
            | EPSILON */
        private void fixed_parameters()
        {
            printIfDebug("fixed_parameters");
            if(pass(typesOptions))
            {
                fixed_parameter();
                fixed_paramaters_p();
            }else{
                //EPSILON
            }
        }

        /*fixed-parameters-p:
            | ',' fixed-parameter fixed-parameters-p
            | EPSILON */
        private void fixed_paramaters_p()
        {
            printIfDebug("fixed_paramaters_p");
            if(pass(TokenType.PUNT_COMMA))
            {
                consumeToken();
                fixed_parameter();
                fixed_paramaters_p();
            }else{
                //EPSILON
            }
        }

        /*fixed-parameter:
	        | type identifier */
        private void fixed_parameter()
        {
            printIfDebug("fixed_parameter");
            if(!pass(typesOptions))
                throwError("type expected");
            types();
            if(!pass(TokenType.ID))
                throwError("identifier expected");
            consumeToken();
        }
    }
}