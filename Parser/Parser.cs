using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.TreeNodes;

namespace Compiler
{
    public partial class Parser
    {
        private Lexer lexer;
        private Token token;
        private bool debug;
        private List<Token> look_ahead;

        private void printIfDebug(string msj)
        {
            if(debug)
                Console.Out.WriteLine(msj+" | "+token.type+" ::= "+token.lexeme);
        }

        public Parser(Lexer lexer)
        {
            debug = false;
            this.lexer = lexer;
            token = lexer.GetNextToken();
            look_ahead = new List<Token>();
        }

        public CompilationUnitNode parse(){
            printIfDebug("parse");
            var compilationNode = compilation_unit();
            if(!pass(TokenType.EOF))
            {
                throwError("end of file token expected.");
            }
            return compilationNode;
        }

        /*compilation-unit:
            | optional-using-directive optional-namespace-or-type-member-declaration
            | optional-namespace-or-type-member-declaration
            | EPSILON */
        private CompilationUnitNode compilation_unit()
        {
            printIfDebug("compilation_unit");
            var usingList = optional_using_directive();
            var compilation = new CompilationUnitNode(usingList);
            optional_namespace_or_type_member_declaration(ref compilation,ref compilation.defaultNamespace);
            return compilation;
        }


        /*optional-using-directive:
            | using-directive
            | EPSILON */
        private List<UsingNode> optional_using_directive()
        {
            printIfDebug("optional_using_directive");
            if(pass(TokenType.RW_USING))
            {
                return using_directive();
            }else{
                return new List<UsingNode>();
            }
        }

        /*type-declaration-list:
            | type-declaration type-declaration-list
            | EPSILON */
        private List<TypeNode> type_declaration_list()
        {
            printIfDebug("type_declaration_list");
            if(pass(encapsulationOptions,typesDeclarationOptions))
            {
                var declaredType = type_declaration();
                var listTypesDeclared = type_declaration_list();
                listTypesDeclared.Insert(0,declaredType);
                return listTypesDeclared;
            }else{
                return new List<TypeNode>();
            }
        }

        /*type-declaration:
	        | encapsulation-modifier group-declaration */
        private TypeNode type_declaration() //TODO: encapsulationModifiers
        {
            printIfDebug("type_declaration");
            if(!pass(encapsulationOptions,typesDeclarationOptions))
            {
                throwError("expected member declaration");
            }
            if(pass(encapsulationOptions))
            {
                encapsulation_modifier();
            }
            return group_declaration();
        }

        /*group-declaration:
            | class-declaration
            | interface-declaration
            | enum-declaration */
        private TypeNode group_declaration() //TODO: Modifiers
        {
            printIfDebug("group_declaration");
            if(pass(TokenType.RW_ABSTRACT,TokenType.RW_CLASS))
            {
                return class_declaration();
            }else if(pass(TokenType.RW_INTERFACE))
            {
                return interface_declaration();
            }else if(pass(TokenType.RW_ENUM))
            {
                return enum_declaration();
            }else{
                throwError("group-declaration expected [class|interface|enum]");
            }
            return null;
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

        /*qualified-identifier:
	        | identifier identifier-attribute */
        private IdNode qualified_identifier()
        {
            if(!pass(TokenType.ID))
                throwError("identifier expected");
            var id = token.lexeme;
            consumeToken();
            var attr = identifier_attribute();
            return new IdNode(id,attr);
        }

        /*identifier-attribute:
            | '.' identifier identifier-attribute
            | EPSILON */
        private List<IdNode> identifier_attribute()
        {
            printIfDebug("identifier_attribute");
            if(pass(TokenType.PUNT_ACCESOR))
            {
                consumeToken();
                if(!pass(TokenType.ID))
                    throwError("identifier expected");
                var idValue = token.lexeme;
                consumeToken();
                var listIdNod = identifier_attribute();
                listIdNod.Insert(0,new IdNode(idValue));
                return listIdNod;
            }else{
                return new List<IdNode>();
            }
        }

        /*identifiers-list:
	        | qualified-identifier identifiers-list-p */
        private void identifiers_list()
        {
            qualified_identifier();
            identifiers_list_p();
        }

        /*identifiers-list-p:
            | ',' qualified-identifier identifiers-list-p
            | EPSILON */
        private void identifiers_list_p()
        {
            if(pass(TokenType.PUNT_COMMA))
            {
                consumeToken();
                qualified_identifier();
                identifiers_list_p();
            }else{
                //EPSILON
            }
        }

        /*type:
            | built-in-type optional-rank-specifier-list
            | qualified-identifier optional-rank-specifier-list */
        private void types()
        {
            printIfDebug("types");
            if(pass(typesOptions) && !pass(TokenType.ID))
            {
                built_in_type();
                optional_rank_specifier_list();
            }else if(pass(TokenType.ID))
            {
                qualified_identifier();
                optional_rank_specifier_list();
            }else{
                throwError("type expected");
            }
        }

        /*built-in-type:
            | "int"
            | "char"
            | "string"
            | "bool"
            | "float" */
        private void built_in_type()
        {
            printIfDebug("built_in_type");
            if(!pass(typesOptions))
                throwError("primary type expected");
            consumeToken();
        }

        /*type-or-void:
            | type
            | "void" */
        private void type_or_void()
        {
            printIfDebug("type_or_void");
            if(pass(TokenType.RW_VOID))
                consumeToken();
            else
                types();
        }

        /*type-or-var:
            | type
            | "var" */
        private void type_or_var()
        {
            printIfDebug("type_or_var");
            if(pass(TokenType.RW_VAR))
                consumeToken();
            else
                types();
        }
    }
}
