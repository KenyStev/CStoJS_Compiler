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
        private List<UsingNode> using_directive()
        {
            printIfDebug("using_directive");
            if(!pass(TokenType.RW_USING))
                throwError("'using' expected");
            var usingToken = token;
            consumeToken();
            var idValue = qualified_identifier();
            if(!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                throwError("; expected");
            consumeToken();
            var usingList = optional_using_directive();
            usingList.Insert(0,new UsingNode(idValue,usingToken));
            return usingList;
        }

        /*optional-namespace-or-type-member-declaration:
            | namespace-or-type-member-declaration
            | EPSILON */
        private void optional_namespace_or_type_member_declaration(ref CompilationUnitNode compilation,ref NamespaceNode currentNamespace)
        {
            printIfDebug("optional_namespace_or_type_member_declaration");
            if(pass(namespaceOption,encapsulationOptions,typesDeclarationOptions))
            {
                namespace_or_type_member_declaration(ref compilation,ref currentNamespace);
            }else{
                //EPSILON
            }
        }

        /*namespace-or-type-member-declaration:
            | namespace-declaration optional-namespace-or-type-member-declaration
            | type-declaration-list optional-namespace-or-type-member-declaration */
        private void namespace_or_type_member_declaration(ref CompilationUnitNode compilation,ref NamespaceNode currentNamespace)
        {
            printIfDebug("namespace_or_type_member_declaration");
            if(pass(TokenType.RW_NAMESPACE))
            {
                var namespaceDeclared = namespace_declaration(ref compilation);
                if(currentNamespace.Identifier.Name != "default")
                {
                    // namespaceDeclared.setParentNamePrefix(currentNamespace.Identifier);
                    namespaceDeclared.setParentNamespace(ref currentNamespace);
                    // namespaceDeclared.addFatherUsings(currentNamespace.usingDirectives);
                }
                // else{
                //     namespaceDeclared.addFatherUsings(compilation.usingDirectives);
                // }
                compilation.addNamespace(namespaceDeclared);
                optional_namespace_or_type_member_declaration(ref compilation, ref currentNamespace);
            }else{
                var listTypeDeclared = type_declaration_list();
                currentNamespace.addTypeList(listTypeDeclared);
                optional_namespace_or_type_member_declaration(ref compilation,ref currentNamespace);
            }
        }

        /*namespace-declaration:
	        | "namespace" qualified-identifier identifier-attribute namespace-body */
        private NamespaceNode namespace_declaration(ref CompilationUnitNode compilation)
        {
            printIfDebug("namespace_declaration");
            if(!pass(TokenType.RW_NAMESPACE))
                throwError("'namespace' expected");
            var namespaceToken = token;
            consumeToken();
            var idNamespace = qualified_identifier();
            if(!pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
                throwError("'{' expected");

            var namespaceDeclaration = new NamespaceNode(idNamespace,namespaceToken);
            namespace_body(ref compilation, ref namespaceDeclaration);
            return namespaceDeclaration;
        }

        /*namespace-body:
	        | '{' optional-using-directive optional-namespace-member-declaration '}' */
        private void namespace_body(ref CompilationUnitNode compilation,ref NamespaceNode currentNamespace)
        {
            printIfDebug("namespace_body");
            if(!pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
                throwError("'{' expected");
            consumeToken();

            if(pass(TokenType.RW_USING))
            {
                var usingDirectives = optional_using_directive();
                currentNamespace.setUsings(usingDirectives);
            }
            if(pass(namespaceOption,encapsulationOptions,typesDeclarationOptions))
            {
                optional_namespace_or_type_member_declaration(ref compilation, ref currentNamespace);
            }else{
                //EPSILON
            }
            if(!pass(TokenType.PUNT_CURLY_BRACKET_CLOSE))
                throwError("'}' expected");
            consumeToken();
        }
    }
}