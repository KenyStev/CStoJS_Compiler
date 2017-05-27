using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.TreeNodes;
using Compiler.TreeNodes.Types;

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
                throwError("'interface' reserved word expected");
            consumeToken();
            if(!pass(TokenType.ID))
                throwError("identifier expected");
            var interfaceID = new IdNode(token.lexeme);
            consumeToken();
            var inheritanceses = inheritance_base();
            var newInterfaceType = interface_body(interfaceID);
            optional_body_end();
            return newInterfaceType;
        }

        /*interface-body:
	        | '{' interface-method-declaration-list '}' */
        private InterfaceTypeNode interface_body(IdNode name)
        {
            printIfDebug("interface_body");
            if(!pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
                throwError("'{' expected");
            consumeToken();
            var methodDeclarationList = interface_method_declaration_list();
            if(!pass(TokenType.PUNT_CURLY_BRACKET_CLOSE))
                throwError("'}' expected");
            consumeToken();
            return new InterfaceTypeNode(name,methodDeclarationList);
        }

        /*interface-method-declaration-list:
            | interface-method-header ';' interface-method-declaration-list
            | EPSILON */
        private List<MethodNode> interface_method_declaration_list()
        {
            printIfDebug("interface_method_declaration_list");
            if(pass(typesOptions,voidOption))
            {
                var methodHeader = interface_method_header();
                if(!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                    throwError("; expected");
                consumeToken();
                var methodList = interface_method_declaration_list();
                methodList.Insert(0,methodHeader);
                return methodList;
            }else{
                return new List<MethodNode>();
            }
        }

        /*interface-method-header:
	        | type-or-void identifier '(' fixed-parameters ')'  */
        private MethodNode interface_method_header() //TODO: method header
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
            return null;
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