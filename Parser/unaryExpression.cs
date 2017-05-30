using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.TreeNodes.Expressions;
using Compiler.TreeNodes.Expressions.UnaryExpressions;
using Compiler.TreeNodes.Expressions.UnaryExpressions.InstanceExpressions;
using Compiler.TreeNodes.Expressions.UnaryExpressions.Literals;
using Compiler.TreeNodes.Expressions.UnaryExpressions.ReferenceAccsess;
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
                Token unaryOperator = token;
                consumeToken();
                var unaryExpression = unary_expression();
                return new UnaryNode(unaryOperator.type,unaryExpression,unaryOperator);
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
                    var targetCastType = types();

                    if (!pass(TokenType.PUNT_PAREN_CLOSE))
                        throwError("')' expected");
                    consumeToken();
                    var exp = primary_expression();
                    return new CastingExpressionNode(targetCastType,exp,targetCastType.token);
                }
                else
                {
                    return primary_expression();
                }
            }else if(pass(unaryExpressionOptions,literalOptions))
            {
                return primary_expression();
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
        private PrimaryExpressionNode primary_expression()
        {
            printIfDebug("primary_expression");
            if(pass(TokenType.RW_NEW))
            {
                consumeToken();
                var instance = instance_expression();
                return primary_expression_p(instance);
            }else if(pass(literalOptions))
            {
                var literalToken = token;
                consumeToken();
                PrimaryExpressionNode literal = null;
                if(literalToken.type==TokenType.LIT_INT)
                    literal = new LiteralIntNode(literalToken.lexeme,literalToken);
                else if(literalToken.type==TokenType.LIT_FLOAT)
                    literal = new LiteralFloatNode(literalToken.lexeme,literalToken);
                else if(literalToken.type==TokenType.LIT_BOOL)
                    literal = new LiteralBoolNode(literalToken.lexeme,literalToken);
                else if(literalToken.type==TokenType.LIT_CHAR)
                    literal = new LiteralCharNode(literalToken.lexeme,literalToken);
                else if(literalToken.type==TokenType.LIT_STRING)
                    literal = new LiteralStringNode(literalToken.lexeme,literalToken);
                return primary_expression_p(literal);
            }else if(pass(TokenType.ID))
            {
                var identifier = new IdNode(token.lexeme,token);
                consumeToken();
                return primary_expression_p(identifier);
            }else if(pass(TokenType.PUNT_PAREN_OPEN))
            {
                consumeToken();
                var exp = expression();
                if(!pass(TokenType.PUNT_PAREN_CLOSE))
                    throwError(") expected");
                consumeToken();
                return primary_expression_p(new GroupedExpressionNode(exp,exp.token));
            }else if(pass(TokenType.RW_THIS))
            {
                consumeToken();
                return primary_expression_p(new ThisReferenceAccsessNode());
            }else if(pass(TokenType.RW_BASE))
            {
                consumeToken();
                return primary_expression_p(new BaseReferenceAccessNode());
            }else{
                throwError("new, literal, identifier, '(' or \"this\" expected");
            }
            return null;
        }

        /*primary-expression-p:
            | '.' identifier primary-expression-p
            | optional-funct-or-array-call primary-expression-p
            | increment-decrement primary-expression-p 
            | EPSILON  */
        private PrimaryExpressionNode primary_expression_p(PrimaryExpressionNode primary)
        {
            printIfDebug("primary_expression_p");
            if(pass(TokenType.PUNT_ACCESOR))
            {
                consumeToken();
                if(!pass(TokenType.ID))
                    throwError("identifier expected");
                var identifier = new IdNode(token.lexeme,token);
                consumeToken();
                return primary_expression_p(new AccessorNode(primary,identifier,identifier.token));
            }else if(pass(TokenType.PUNT_PAREN_OPEN,TokenType.PUNT_SQUARE_BRACKET_OPEN))
            {
                var accessOrCall = optional_funct_or_array_call(primary);
                return primary_expression_p(accessOrCall);
            }else if(pass(TokenType.OP_PLUS_PLUS,TokenType.OP_MINUS_MINUS))
            {
                var operatorToken = token;
                consumeToken();
                return primary_expression_p(new PostAdditiveExpressionNode(primary,operatorToken.type,operatorToken));
            }else{
                return primary;
            }
        }

        /*optional-funct-or-array-call:
            | '(' argument-list ')'
            | optional-array-access-list
            | EPSILON */
        private PrimaryExpressionNode optional_funct_or_array_call(PrimaryExpressionNode identifier)
        {
            printIfDebug("optional_funct_or_array_call");
            var accessOrCallToken = token;
            if(pass(TokenType.PUNT_PAREN_OPEN))
            {
                consumeToken();
                var arguments = argument_list();
                if(!pass(TokenType.PUNT_PAREN_CLOSE))
                    throwError(") expected");
                consumeToken();
                return new FunctionCallExpressionNode(identifier,arguments,accessOrCallToken);
            }else if(pass(TokenType.PUNT_SQUARE_BRACKET_OPEN))
            {
                var arrayAccessList = optional_array_access_list();
                return new ArrayAccessExpressionNode(identifier,arrayAccessList,accessOrCallToken);
            }else{
                return identifier;
            }
        }

        /*optional-array-access-list:
            | '[' expression-list ']' optional-array-access-list
            | EPSILON */
        private List<List<ExpressionNode>> optional_array_access_list()
        {
            printIfDebug("optional_array_access_list");
            if(pass(TokenType.PUNT_SQUARE_BRACKET_OPEN))
            {
                consumeToken();
                var expressionList = expression_list();
                if(!pass(TokenType.PUNT_SQUARE_BRACKET_CLOSE))
                    throwError("] expected");
                consumeToken();
                var arrayAccessList = optional_array_access_list();
                arrayAccessList.Insert(0,expressionList);
                return arrayAccessList;
            }else{
                return new List<List<ExpressionNode>>();
            }
        }

        /*expression-list:
	        | expression optional-expression-list */
        private List<ExpressionNode> expression_list()
        {
            printIfDebug("expression_list");
            var exp = expression();
            var expList = optional_expression_list();
            expList.Insert(0,exp);
            return expList;
        }

        /*optional-expression-list:
            | ',' expression-list
            | EPSILON */
        private List<ExpressionNode> optional_expression_list()
        {
            printIfDebug("optional_expression_list");
            if(pass(TokenType.PUNT_COMMA))
            {
                consumeToken();
                return expression_list();
            }else{
                return new List<ExpressionNode>();
            }
        }

        /*instance-expression:
	        | type instance-expression-factorized */
        private InstanceExpressionNode instance_expression()
        {
            printIfDebug("instance_expression");
            if(!pass(typesOptions))
                throwError("type expected");
            TypeNode type = null;
            var identifierToken = token;
            if(pass(TokenType.ID))
            {
                var identifier = qualified_identifier();
                type = new AbstractTypeNode(identifier,identifierToken);
            }else{
                type = new PrimitiveTypeNode(token);
                consumeToken();
            }
            return instance_expression_factorized(type,identifierToken);
        }

        /*instance-expression-factorized:
            | '[' instance-expression-factorized-p 
            | '(' argument-list ')' */
        private InstanceExpressionNode instance_expression_factorized(TypeNode type,Token identifierToken)
        {
            printIfDebug("instance_expression_factorized");
            if (pass(TokenType.PUNT_SQUARE_BRACKET_OPEN))
            {
                var instanceToken = token;
                consumeToken();
                return instance_expression_factorized_p(type,identifierToken,instanceToken);
            }else if (pass(TokenType.PUNT_PAREN_OPEN))
            {
                consumeToken();
                var arguments = argument_list();

                if (!pass(TokenType.PUNT_PAREN_CLOSE))
                    throwError("')' expected");
                consumeToken();
                return new ClassInstantioationNode(type,arguments,identifierToken);
            }
            else
            {
                throwError("'[' or '(' expected");
                return null;
            }
        }

        /*instance-expression-factorized-p:
            | expression-list ']' optional-rank-specifier-list optional-array-initializer
            | rank-specifier-list optional-array-initializer */
        private ArrayInstantiationNode instance_expression_factorized_p(TypeNode type,Token identifierToken,Token instanceToken)
        {
            printIfDebug("instance_expression_factorized_p");
            if(pass(expressionOptions()))
            {
                var primaryExpBrackets = expression_list();
                if (!pass(TokenType.PUNT_SQUARE_BRACKET_CLOSE))
                    throwError("']' expected");
                consumeToken();

                var initializationToken = token;
                var rankList = optional_rank_specifier_list();
                var arrayType = new ArrayTypeNode(type,rankList,identifierToken);
                var initialization = optional_array_initializer();
                return new ArrayInstantiationNode(type,primaryExpBrackets,arrayType,initialization,instanceToken);
            }else if (pass(TokenType.PUNT_COMMA,TokenType.PUNT_SQUARE_BRACKET_CLOSE))
            {
                var rankList = rank_specifier_list();
                var arrayInitializer = array_initializer();
                var arrayType = new ArrayTypeNode(type,rankList,identifierToken);
                return new ArrayInstantiationNode(type,arrayType,arrayInitializer,instanceToken);
            }
            else
            {
                throwError("expression or rank specifier ','");
                return null;
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
            var rankToken = token;
            var dimensions = optional_comma_list();
            if(!pass(TokenType.PUNT_SQUARE_BRACKET_CLOSE))
                throwError("] expected");
            consumeToken();
            return new MultidimensionArrayTypeNode(dimensions,rankToken);
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
        private ArrayInitializerNode optional_array_initializer()
        {
            printIfDebug("optional_array_initializer");
            if(pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
            {
                return array_initializer();
            }else{
                return null;
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