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
        private TypeNode type_declaration()
        {
            printIfDebug("type_declaration");
            if(!pass(encapsulationOptions,typesDeclarationOptions))
            {
                throwError("expected member declaration");
            }
            
            var encapMod = encapsulation_modifier();
            
            return group_declaration(encapMod);
        }

        /*group-declaration:
            | class-declaration
            | interface-declaration
            | enum-declaration */
        private TypeNode group_declaration(EncapsulationNode encapMod)
        {
            printIfDebug("group_declaration");
            TypeNode typeDeclaration = null;
            if(pass(TokenType.RW_ABSTRACT,TokenType.RW_CLASS))
            {
                typeDeclaration = class_declaration();
            }else if(pass(TokenType.RW_INTERFACE))
            {
                typeDeclaration = interface_declaration();
            }else if(pass(TokenType.RW_ENUM))
            {
                typeDeclaration = enum_declaration();
            }else{
                throwError("group-declaration expected [class|interface|enum]");
            }
            if(typeDeclaration!=null)
                typeDeclaration.setEncapsulationMode(encapMod);
            return typeDeclaration;
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
        private List<IdNode> identifiers_list()
        {
            var idnode = qualified_identifier();
            var lisIdNodes = identifiers_list_p();
            lisIdNodes.Insert(0,idnode);
            return lisIdNodes;
        }

        /*identifiers-list-p:
            | ',' qualified-identifier identifiers-list-p
            | EPSILON */
        private List<IdNode> identifiers_list_p()
        {
            if(pass(TokenType.PUNT_COMMA))
            {
                consumeToken();
                var idnode = qualified_identifier();
                var listIdNode = identifiers_list_p();
                listIdNode.Insert(0,idnode);
                return listIdNode;
            }else{
                return new List<IdNode>();
            }
        }

        /*type:
            | built-in-type optional-rank-specifier-list
            | qualified-identifier optional-rank-specifier-list */
        private TypeNode types()
        {
            printIfDebug("types");
            if(pass(typesOptions) && !pass(TokenType.ID))
            {
                var primitiveType = built_in_type();
                var newMultArrayTypeList = optional_rank_specifier_list();
                if(newMultArrayTypeList.Count>0)
                    return new ArrayTypeNode(primitiveType,newMultArrayTypeList);
                else
                    return primitiveType;
            }else if(pass(TokenType.ID))
            {
                var typeName = qualified_identifier();
                var abstractType = new AbstractTypeNode(typeName);
                var newMultArrayTypeList = optional_rank_specifier_list();
                if(newMultArrayTypeList.Count>0)
                    return new ArrayTypeNode(abstractType,newMultArrayTypeList);
                else
                    return abstractType;
            }else{
                throwError("type expected");
            }
            return null;
        }

        /*built-in-type:
            | "int"
            | "char"
            | "string"
            | "bool"
            | "float" */
        private PrimitiveTypeNode built_in_type()
        {
            printIfDebug("built_in_type");
            if(!pass(typesOptions))
                throwError("primary type expected");
            var type = token.type;
            consumeToken();
            return new PrimitiveTypeNode(type);
        }

        /*type-or-void:
            | type
            | "void" */
        private TypeNode type_or_void()
        {
            printIfDebug("type_or_void");
            if(pass(TokenType.RW_VOID))
            {
                consumeToken();
                return new VoidTypeNode();
            }
            return types();
        }

        /*type-or-var:
            | type
            | "var" */
        private TypeNode type_or_var()
        {
            printIfDebug("type_or_var");
            if(pass(TokenType.RW_VAR))
            {
                consumeToken();
                return new VarTypeNode();
            }
            return types();
        }
    }
}
