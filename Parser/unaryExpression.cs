using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.TreeNodes.Expressions.UnaryExpressions;
using Compiler.TreeNodes.Types;

namespace Compiler
{
    public partial class Parser
    {
        /*unary-expression:
            | expression-unary-operator unary-expression
            | '(' type ')' primary-expression
            | primary-expression */
        private UnaryExpressionNode unary_expression()
        {
            printIfDebug("unary_expression");
            if(pass(unaryOperatorOptions))
            {
                consumeToken();
                unary_expression();
            }else if(pass(TokenType.PUNT_PAREN_OPEN))
            {
                addLookAhead(lexer.GetNextToken());
                int first = look_ahead.Count() - 1;
                Token placehold = look_ahead[look_ahead.Count() - 1];
                bool accept = false;
                while (typesOptions.Contains(placehold.type) || placehold.type == TokenType.PUNT_ACCESOR
                    || placehold.type == TokenType.PUNT_SQUARE_BRACKET_OPEN || placehold.type == TokenType.PUNT_SQUARE_BRACKET_CLOSE
                    || placehold.type == TokenType.OP_LESS_THAN || placehold.type == TokenType.OP_MORE_THAN
                    || placehold.type == TokenType.PUNT_COMMA)
                {
                    addLookAhead(lexer.GetNextToken());
                    placehold = look_ahead[look_ahead.Count() - 1];
                    accept = true;
                }
                
                if (typesOptions.Contains(look_ahead[first].type) && accept && 
                    (placehold.type == TokenType.PUNT_PAREN_CLOSE))
                {
                    consumeToken();
                    if (!pass(typesOptions))
                        throwError("type expected");
                    types();

                    if (!pass(TokenType.PUNT_PAREN_CLOSE))
                        throwError("')' expected");
                    consumeToken();
                    primary_expression();
                }
                else
                {
                    primary_expression();
                }
            }else if(pass(unaryExpressionOptions,literalOptions))
            {
                primary_expression();
            }else{
                throwError("unary-operator, casting or primary-expression expected");
            }
            return null;
        }

        /*primary-expression:
            | "new" instance-expression primary-expression-p
            | literal primary-expression-p
            | identifier primary-expression-p
            | '(' expression ')' primary-expression-p
            | "this" primary-expression-p
            | "base" primary-expression-p */
        private void primary_expression()
        {
            printIfDebug("primary_expression");
            if(pass(TokenType.RW_NEW))
            {
                consumeToken();
                instance_expression();
                if(pass(primaryOptionsPrime))
                    primary_expression_p();
            }else if(pass(literalOptions))
            {
                consumeToken();
                if(pass(primaryOptionsPrime))
                    primary_expression_p();
            }else if(pass(TokenType.ID))
            {
                consumeToken();
                if(pass(primaryOptionsPrime))
                    primary_expression_p();
            }else if(pass(TokenType.PUNT_PAREN_OPEN))
            {
                consumeToken();
                expression();
                if(!pass(TokenType.PUNT_PAREN_CLOSE))
                    throwError(") expected");
                consumeToken();
                if(pass(primaryOptionsPrime))
                    primary_expression_p();
            }else if(pass(TokenType.RW_THIS))
            {
                consumeToken();
                if(pass(primaryOptionsPrime))
                    primary_expression_p();
            }else if(pass(TokenType.RW_BASE))
            {
                consumeToken();
                if(pass(primaryOptionsPrime))
                    primary_expression_p();
            }else{
                throwError("new, literal, identifier, '(' or \"this\" expected");
            }
        }

        /*primary-expression-p:
            | '.' identifier primary-expression-p
            | optional-funct-or-array-call primary-expression-p
            | increment-decrement primary-expression-p 
            | EPSILON  */
        private void primary_expression_p()
        {
            printIfDebug("primary_expression_p");
            if(pass(TokenType.PUNT_ACCESOR))
            {
                consumeToken();
                if(!pass(TokenType.ID))
                    throwError("identifier expected");
                consumeToken();
                primary_expression_p();
            }else if(pass(TokenType.PUNT_PAREN_OPEN,TokenType.PUNT_SQUARE_BRACKET_OPEN))
            {
                optional_funct_or_array_call();
                primary_expression_p();
            }else if(pass(TokenType.OP_PLUS_PLUS,TokenType.OP_MINUS_MINUS))
            {
                consumeToken();
                primary_expression_p();
            }else{
                //EPSILON
            }
        }

        /*optional-funct-or-array-call:
            | '(' argument-list ')'
            | optional-array-access-list
            | EPSILON */
        private void optional_funct_or_array_call()
        {
            printIfDebug("optional_funct_or_array_call");
            if(pass(TokenType.PUNT_PAREN_OPEN))
            {
                consumeToken();
                argument_list();
                if(!pass(TokenType.PUNT_PAREN_CLOSE))
                    throwError(") expected");
                consumeToken();
            }else if(pass(TokenType.PUNT_SQUARE_BRACKET_OPEN))
            {
                optional_array_access_list();
            }else{
                //EPSILON
            }
        }

        /*optional-array-access-list:
            | '[' expression-list ']' optional-array-access-list
            | EPSILON */
        private void optional_array_access_list()
        {
            printIfDebug("optional_array_access_list");
            if(pass(TokenType.PUNT_SQUARE_BRACKET_OPEN))
            {
                consumeToken();
                expression_list();
                if(!pass(TokenType.PUNT_SQUARE_BRACKET_CLOSE))
                    throwError("] expected");
                consumeToken();
                optional_array_access_list();
            }else{
                //EPSILON
            }
        }

        /*expression-list:
	        | expression optional-expression-list */
        private void expression_list()
        {
            printIfDebug("expression_list");
            expression();
            optional_expression_list();
        }

        /*optional-expression-list:
            | ',' expression-list
            | EPSILON */
        private void optional_expression_list()
        {
            printIfDebug("optional_expression_list");
            if(pass(TokenType.PUNT_COMMA))
            {
                consumeToken();
                expression_list();
            }else{
                //EPSILON
            }
        }

        /*instance-expression:
	        | type instance-expression-factorized */
        private void instance_expression()
        {
            printIfDebug("instance_expression");
            if(!pass(typesOptions))
                throwError("type expected");
            if(pass(TokenType.ID))
            {
                qualified_identifier();
            }else{
                consumeToken();
            }
            instance_expression_factorized();
        }

        /*instance-expression-factorized:
            | '[' instance-expression-factorized-p 
            | '(' argument-list ')' */
        private void instance_expression_factorized()
        {
            printIfDebug("instance_expression_factorized");
            if (pass(TokenType.PUNT_SQUARE_BRACKET_OPEN))
            {
                consumeToken();
                instance_expression_factorized_p();
            }else if (pass(TokenType.PUNT_PAREN_OPEN))
            {
                consumeToken();
                argument_list();

                if (!pass(TokenType.PUNT_PAREN_CLOSE))
                    throwError("')' expected");
                consumeToken();
            }
            else
            {
                throwError("'[' or '(' expected");
            }
        }

        /*instance-expression-factorized-p:
            | expression-list ']' optional-rank-specifier-list optional-array-initializer
            | rank-specifier-list optional-array-initializer */
        private void instance_expression_factorized_p()
        {
            printIfDebug("instance_expression_factorized_p");
            if(pass(expressionOptions()))
            {
                expression_list();
                if (!pass(TokenType.PUNT_SQUARE_BRACKET_CLOSE))
                    throwError("']' expected");
                consumeToken();

                optional_rank_specifier_list();
                optional_array_initializer();
            }else if (pass(TokenType.PUNT_COMMA,TokenType.PUNT_SQUARE_BRACKET_CLOSE))
            {
                rank_specifier_list();
                array_initializer();
            }
            else
            {
                throwError("expression or rank specifier ','");
            }
        }

        /*rank-specifier-list: 
	        | rank-specifier optional-rank-specifier-list */
        private List<MultidimensionArrayTypeNode> rank_specifier_list()
        {
            printIfDebug("rank_specifier_list");
            var array = rank_specifier();
            var arrayList = optional_rank_specifier_list();
            arrayList.Insert(0,array);
            return arrayList;
        }

        /*rank-specifier:
	        | optional-comma-list ']' */
        private MultidimensionArrayTypeNode rank_specifier()
        {
            printIfDebug("rank_specifier");
            var dimensions = optional_comma_list();
            if(!pass(TokenType.PUNT_SQUARE_BRACKET_CLOSE))
                throwError("] expected");
            consumeToken();
            return new MultidimensionArrayTypeNode(dimensions);
        }

        /*optional-comma-list:
            | ',' optional-comma-list
            | EPSILON */
        private int optional_comma_list()
        {
            if(pass(TokenType.PUNT_COMMA))
            {
                consumeToken();
                return 1 + optional_comma_list();
            }else{
                return 1;
            }
        }

        /*optional-array-initializer:
            | array-initializer
            | EPSILON */
        private void optional_array_initializer()
        {
            printIfDebug("optional_array_initializer");
            if(pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
            {
                array_initializer();
            }else{
                //EPSILON
            }
        }

        /*optional-rank-specifier-list:
            | '[' rank-specifier-list
            | EPSILON 	 */
        private List<MultidimensionArrayTypeNode> optional_rank_specifier_list()
        {
            printIfDebug("optional_rank_specifier_list");
            if(pass(TokenType.PUNT_SQUARE_BRACKET_OPEN))
            {
                consumeToken();
                return rank_specifier_list();
            }else{
                return new List<MultidimensionArrayTypeNode>(); 
            }
        }
    }
}