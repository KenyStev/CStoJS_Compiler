using System;
using System.Linq;

namespace Compiler
{
    public partial class Parser
    {
        private Lexer lexer;
        private Token token;
        private bool debug;

        private void printIfDebug(string msj)
        {
            if(debug)
                Console.Out.WriteLine(msj+" ==> "+token.type+" <==");
        }

        public Parser(Lexer lexer)
        {
            debug = true;
            this.lexer = lexer;
            token = lexer.GetNextToken();
        }

        public void parse(){
            printIfDebug("parse");
            compilation_unit();
            if(!pass(TokenType.EOF))
            {
                throwError("end of file token expected.");
            }
        }

        /*compilation-unit:
            | optional-using-directive optional-namespace-member-declaration
            | optional-namespace-member-declaration
            | EPSILON */
        private void compilation_unit()
        {
            printIfDebug("compilation_unit");
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
        }


        /*optional-using-directive:
            | using-directive
            | EPSILON */
        private void optional_using_directive()
        {
            printIfDebug("optional_using_directive");
            if(pass(TokenType.RW_USING))
            {
                using_directive();
            }else{
                //EPSILON
            }
        }

        /*type-declaration-list:
            | type-declaration type-declaration-list
            | EPSILON */
        private void type_declaration_list()
        {
            printIfDebug("type_declaration_list");
            if(pass(encapsulationTypes,typesdeclarationOptions))
            {
                type_declaration();
                type_declaration_list();
            }else{
                //EPSILON
            }
        }

        /*type-declaration:
	        | encapsulation-modifier group-declaration */
        private void type_declaration()
        {
            printIfDebug("type_declaration");
             if(!pass(encapsulationTypes,typesdeclarationOptions))
            {
                throwError("expected member declaration");
            }
            if(pass(encapsulationTypes))
            {
                encapsulation_modifier();
            }
            if(pass(typesdeclarationOptions))
            {
                group_declaration();
            }
        }

        /*group-declaration:
            | class-declaration
            | interface-declaration
            | enum-declaration */
        private void group_declaration()
        {
            printIfDebug("group_declaration");
            if(pass(TokenType.RW_ABSTRACT,TokenType.RW_CLASS))
            {
                class_declaration();
            }else if(pass(TokenType.RW_INTERFACE))
            {
                interface_declaration();
            }else if(pass(TokenType.RW_ENUM))
            {
                enum_declaration();
            }else{
                throwError("group-declaration expected [class|interface|enum]");
            }
        }

        /*optional-body-end:
            | ';'
            | EPSILON */
        private void optional_body_end()
        {
            printIfDebug("optional_body_end");
            if(pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
            {
                consumeToken();
            }else{
                //EPSILON
            }
            
        }

        /*identifier-attribute:
            | '.' identifier identifier-attribute
            | EPSILON */
        private void identifier_attribute()
        {
            printIfDebug("identifier_attribute");
            if(pass(TokenType.PUNT_ACCESOR))
            {
                consumeToken();
                if(!pass(TokenType.ID))
                    throwError("identifier expected");
                consumeToken();
                identifier_attribute();
            }else{
                //EPSILON
            }
        }

        /*identifiers-list:
	        | identifier identifiers-list-p */
        private void identifiers_list()
        {
            if(!pass(TokenType.ID))
                throwError("identifier expected");
            consumeToken();
            identifiers_list_p();
        }

        /*identifiers-list-p:
            | ',' identifier identifiers-list-p
            | EPSILON */
        private void identifiers_list_p()
        {
            if(pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
            {
                consumeToken();
                identifiers_list();
            }else{
                //EPSILON
            }
        }
    }
}
