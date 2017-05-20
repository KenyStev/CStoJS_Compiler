using System;
using System.Linq;

namespace Compiler
{
    public partial class Parser
    {
        /*using-directive:
	        | "using" identifier identifier-attribute ';' optional-using-directive */
        private void using_directive()
        {
            printIfDebug("using_directive");
            if(!pass(TokenType.RW_USING))
                throwError("'using' expected");
            consumeToken();
            if(!pass(TokenType.ID))
                throwError("identifier expected");
            consumeToken();
            identifier_attribute();
            if(!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                throwError("; expected");
            consumeToken();
            optional_using_directive();
        }

        /*optional-namespace-member-declaration:
            | namespace-member-declaration
            | EPSILON */
        private void optional_namespace_member_declaration()
        {
            printIfDebug("optional_namespace_member_declaration");
            TokenType[] namespaceType = {TokenType.RW_NAMESPACE};
            if(pass(namespaceType,encapsulationTypes,typesdeclarationOptions))
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
	        | "namespace" identifier identifier-attribute namespace-body */
        private void namespace_declaration()
        {
            printIfDebug("namespace_declaration");
            if(!pass(TokenType.RW_NAMESPACE))
                throwError("'namespace' expected");
            consumeToken();
            if(!pass(TokenType.ID))
                throwError("identifier expected");
            consumeToken();
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
            TokenType[] namespaceType = {TokenType.RW_NAMESPACE};
            if(pass(namespaceType,encapsulationTypes,typesdeclarationOptions))
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