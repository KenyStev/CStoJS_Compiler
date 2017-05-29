using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.TreeNodes;
using Compiler.TreeNodes.Types;
using Compiler.TreeNodes.Expressions.UnaryExpressions;

namespace Compiler
{
    public partial class Parser
    {
        /*interface-declaration: 
	        | "interface" identifier inheritance-base interface-body optional-body-end */
        private InterfaceTypeNode interface_declaration()
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
            newInterfaceType.setInheritance(inheritanceses);
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
        private List<MethodHeaderNode> interface_method_declaration_list()
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
                return new List<MethodHeaderNode>();
            }
        }

        /*interface-method-header:
	        | type-or-void identifier '(' fixed-parameters ')'  */
        private MethodHeaderNode interface_method_header()
        {
            printIfDebug("interface_method_header");
            if(!pass(typesOptions,voidOption))
                throwError("type-or-void expected");
            var typeNode = type_or_void();
            var returnType = new ReturnTypeNode(typeNode,typeNode is VoidTypeNode);
            if(!pass(TokenType.ID))
                throwError("identifier expected");
            var name = new IdNode(token.lexeme);
            consumeToken();
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            consumeToken();
            List<ParameterNode> fixedParams = null;
            if(pass(typesOptions))
                fixedParams = fixed_parameters();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
            return new MethodHeaderNode(returnType,name,fixedParams);
        }

        /*fixed-parameters:
            | fixed-parameter fixed-paramaters-p
            | EPSILON */
        private List<ParameterNode> fixed_parameters()
        {
            printIfDebug("fixed_parameters");
            if(pass(typesOptions))
            {
                var param = fixed_parameter();
                var parameters = fixed_paramaters_p();
                parameters.Insert(0,param);
                return parameters;
            }else{
                return new List<ParameterNode>();
            }
        }

        /*fixed-parameters-p:
            | ',' fixed-parameter fixed-parameters-p
            | EPSILON */
        private List<ParameterNode> fixed_paramaters_p()
        {
            printIfDebug("fixed_paramaters_p");
            if(pass(TokenType.PUNT_COMMA))
            {
                consumeToken();
                var param = fixed_parameter();
                var parameters = fixed_paramaters_p();
                parameters.Insert(0,param);
                return parameters;
            }else{
                return new List<ParameterNode>();
            }
        }

        /*fixed-parameter:
	        | type identifier */
        private ParameterNode fixed_parameter()
        {
            printIfDebug("fixed_parameter");
            if(!pass(typesOptions))
                throwError("type expected");
            var type = types();
            if(!pass(TokenType.ID))
                throwError("identifier expected");
            var paramName = new IdNode(token.lexeme);
            consumeToken();
            return new ParameterNode(type,paramName);
        }
    }
}