using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.TreeNodes;
using Compiler.TreeNodes.Expressions;
using Compiler.TreeNodes.Statements;
using Compiler.TreeNodes.Types;

namespace Compiler
{
    public partial class Parser
    {
        /*enum-declaration: 
	        | "enum" identifier enum-body optional-body-end */
        private EnumTypeNode enum_declaration()
        {
            printIfDebug("enum_declaration");
            if(!pass(TokenType.RW_ENUM))
                throwError("'enum' expected");
            consumeToken();
            if(!pass(TokenType.ID))
                throwError("identifier expected");
            var idNode = new IdNode(token.lexeme);
            consumeToken();
            var enumDef = enum_body(idNode);
            optional_body_end();
            return enumDef;//enumDef;
        }

        /*enum-body:
	        | '{' optional-assignable-identifiers-list '}' */
        private EnumTypeNode enum_body(IdNode idnode)
        {
            printIfDebug("enum_body");
            if(!pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
                throwError("'{' expected");
            consumeToken();
            var enumerableList = optional_assignable_identifiers_list(0);
            if(!pass(TokenType.PUNT_CURLY_BRACKET_CLOSE))
                throwError("'}' expected");
            consumeToken();
            return new EnumTypeNode(idnode,enumerableList);
        }

        /*optional-assignable-identifiers-list:
            | identifier assignment-options
            | EPSILON */
        private List<EnumNode> optional_assignable_identifiers_list(int counter)
        {
            printIfDebug("optional_assignable_identifiers_list");
            if(pass(TokenType.ID))
            {
                var idListed = new IdNode(token.lexeme);
                consumeToken();
                return assignment_options(idListed,counter);
            }else{
                return new List<EnumNode>();
            }
        }

        /*assignment-options:
            | optional-assignable-identifiers-list-p
            | '=' expression optional-assignable-identifiers-list-p
            | EPSILON */
        private List<EnumNode> assignment_options(IdNode currentId,int counter)
        {
            printIfDebug("assignment_options");
            if(pass(TokenType.PUNT_COMMA))
            {
                var newEntryEnum = new EnumNode(currentId,new LiteralIntNode(counter));
                return optional_assignable_identifiers_list_p(newEntryEnum,counter+1);
            }else if(pass(TokenType.OP_ASSIGN))
            {
                consumeToken();
                var exp = expression();
                var assignExpr = new EnumNode(currentId,exp);
                // var nextVal = new SumNode(exp,new LiteralIntNode(1)); //TODO: evaluate and send as parameter
                return optional_assignable_identifiers_list_p(assignExpr,counter+1);
            }else{
                var newListEnumerables = new List<EnumNode>();
                var newEntryEnum = new EnumNode(currentId,new LiteralIntNode(counter));
                newListEnumerables.Add(newEntryEnum);
                return newListEnumerables;
            }
        }

        /*optional-assignable-identifiers-list-p:
            | ',' optional-assignable-identifiers-list
            | EPSILON */
        private List<EnumNode> optional_assignable_identifiers_list_p(EnumNode newEntryEnum,int counter)
        {
            printIfDebug("optional_assignable_identifiers_list_p");
            if(pass(TokenType.PUNT_COMMA))
            {
                consumeToken();
                var listEntries = optional_assignable_identifiers_list(counter);
                listEntries.Insert(0,newEntryEnum);
                return listEntries;
            }else{
                var newListEnumerables = new List<EnumNode>();
                newListEnumerables.Add(newEntryEnum);
                return newListEnumerables;
            }
        }
    }
}