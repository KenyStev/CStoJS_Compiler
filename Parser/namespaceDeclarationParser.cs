using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.TreeNodes;

namespace Compiler
{
    public partial class Parser
    {
        /*using-directive:
	        | "using" qualified-identifier ';' optional-using-directive */
        private List<UsingDeclarationStatement> using_directive()
        {
            printIfDebug("using_directive");
            if(!pass(TokenType.RW_USING))
                throwError("'using' expected");
            consumeToken();
            var idValue = qualified_identifier();
            if(!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                throwError("; expected");
            consumeToken();
            var usingList = optional_using_directive();
            usingList.Insert(0,new UsingDeclarationStatement(idValue));
            return usingList;
        }

        /*optional-namespace-member-declaration:
            | namespace-member-declaration
            | EPSILON */
        private void optional_namespace_member_declaration()
        {
            printIfDebug("optional_namespace_member_declaration");
            if(pass(namespaceOption,encapsulationOptions,typesDeclarationOptions))
            {
                namespace_member_declaration();
            }else{
                //EPSILON
            }
        }

        /*namespace-member-declaration:
            | namespace-declaration optional-namespace-member-declaration
            | type-declaration-list optional-namespace-member-declaration */
        private void namespace_member_declaration()
        {
            printIfDebug("namespace_member_declaration");
            if(pass(TokenType.RW_NAMESPACE))
            {
                namespace_declaration();
                optional_namespace_member_declaration();
            }else{
                type_declaration_list();
                optional_namespace_member_declaration();
            }
        }

        /*namespace-declaration:
	        | "namespace" qualified-identifier identifier-attribute namespace-body */
        private void namespace_declaration()
        {
            printIfDebug("namespace_declaration");
            if(!pass(TokenType.RW_NAMESPACE))
                throwError("'namespace' expected");
            consumeToken();
            qualified_identifier();
            identifier_attribute();
            if(!pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
                throwError("'{' expected");
            namespace_body();
        }

        /*namespace-body:
	        | '{' optional-using-directive optional-namespace-member-declaration '}' */
        private void namespace_body()
        {
            printIfDebug("namespace_body");
            if(!pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
                throwError("'{' expected");
            consumeToken();

            if(pass(TokenType.RW_USING))
            {
                optional_using_directive();
            }
            if(pass(namespaceOption,encapsulationOptions,typesDeclarationOptions))
            {
                optional_namespace_member_declaration();
            }else{
                //EPSILON
            }
            if(!pass(TokenType.PUNT_CURLY_BRACKET_CLOSE))
                throwError("'}' expected");
            consumeToken();
        }
    }
}