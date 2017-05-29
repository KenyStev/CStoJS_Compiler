using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.TreeNodes.Expressions;
using Compiler.TreeNodes.Statements;

namespace Compiler
{
    public partial class Parser
    {
        /*embedded-statement:
            | maybe-empty-block
            | statement-expression ';'
            | selection-statement
            | iteration-statement
            | jump-statement ';' */
        private EmbeddedStatementNode embedded_statement() //TODO
        {
            printIfDebug("embedded_statement");

            if(!pass(embededOptions()))
                throwError("{}, ;, statement, if, switch, for, foreach, while, do-while, continue or return expected");

            if(pass(maybeEmptyBlockOptions))
            {
                return maybe_empty_block();
            }else if(pass(unaryExpressionOptions,unaryOperatorOptions,literalOptions))
            {
                var stmt = statement_expression();
                if(!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                    throwError("; expected");
                consumeToken();
                return stmt;
            }else if(pass(selectionsOptionsStatements))
            {
                return selection_statement();
            }else if(pass(iteratorsOptionsStatements)) //TODO
            {
                // return 
                iteration_statement();
            }else if(pass(jumpsOptionsStatements)) //TODO
            {
                EmbeddedStatementNode jumpStmt = null;
                jump_statement();
                if(!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                    throwError("; expected");
                consumeToken();
                return jumpStmt;
            }
            return null;
        }

        /*maybe-empty-block:
            | '{' optional-statement-list '}'
            | ';' */
        private StatementBlockNode maybe_empty_block()
        {
            printIfDebug("maybe_empty_block");
            if(!pass(TokenType.PUNT_CURLY_BRACKET_OPEN,TokenType.PUNT_END_STATEMENT_SEMICOLON))
                throwError("'{' or ';' expected");
            
            if(pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
            {
                consumeToken();
                var statements = optional_statement_list();
                if(!pass(TokenType.PUNT_CURLY_BRACKET_CLOSE))
                    throwError("'}' expected");
                consumeToken();
                return new StatementBlockNode(statements);
            }else if(pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
            {
                consumeToken();
            }
            return null;
        }

        /*jump-statement:
            | "break"
            | "continue"
            | "return" optional-expression */
        private void jump_statement()
        {
            printIfDebug("jump_statement");
            if(!pass(jumpsOptionsStatements))
                throwError("break, continue or return expected");
            Token old = token;
            consumeToken();
            if(old.type==TokenType.RW_RETURN)
                optional_expression();
        }

        /*optional-expression: 
            | expression
            | EPSILON */
        private void optional_expression()
        {
            printIfDebug("optional_expression");
            if(pass(expressionOptions()))
            {
                expression();
            }else{
                //EPSILON
            }
        }

        /*iteration-statement:
            | while-statement
            | do-statement
            | for-statement
            | foreach-statement */
        private void iteration_statement()
        {
            printIfDebug("iteration_statement");
            if(pass(TokenType.RW_WHILE))
            {
                while_statement();
            }else if(pass(TokenType.RW_DO))
            {
                do_statement();
            }else if(pass(TokenType.RW_FOR))
            {
                for_statement();
            }else if(pass(TokenType.RW_FOREACH))
            {
                foreach_statement();
            }else{
                throwError("while, do-while, for or foreach conditional statement expected");
            }
        }

        /*while-statement:
	        | "while" '(' expression ')' embedded-statement */
        private void while_statement()
        {
            printIfDebug("while_statement");
            if(!pass(TokenType.RW_WHILE))
                throwError("'while' reserved word expected");
            consumeToken();
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            consumeToken();
            expression();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
            embedded_statement();
        }

        /*do-statement: 
	        | "do" embedded-statement "while" '(' expression ')' ';' */
        private void do_statement()
        {
            printIfDebug("do_statement");
            if(!pass(TokenType.RW_DO))
                throwError("'do' reserved word expected");
            consumeToken();
            embedded_statement();
            if(!pass(TokenType.RW_WHILE))
                throwError("'while' reserved word expected");
            consumeToken();
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            consumeToken();
            expression();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
            if(!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                throwError("; expected");
            consumeToken();
        }

        /*for-statement:
	        | "for" '(' optional-for-initializer ';' optional-expression ';' optional-statement-expression-list ')' embedded-statement */
        private void for_statement()
        {
            printIfDebug("for_statement");
            if(!pass(TokenType.RW_FOR))
                throwError("'for' reserved word expected");
            consumeToken();
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            consumeToken();
            //initializer
            optional_for_initializer();
            if(!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                throwError("';' expected");
            consumeToken();
            //expresion
            optional_expression();
            if(!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                throwError("';' expected");
            consumeToken();
            //statementlist
            optional_statement_expression_list();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
            embedded_statement();
        }

        /*optional-for-initializer:
            | local-variable-declaration
            | statement-expression-list
            | EPSILON */
        private void optional_for_initializer()
        {
            printIfDebug("optional_for_initializer");
            addLookAhead(lexer.GetNextToken());
            int look_ahead_index = look_ahead.Count()-1;
            addLookAhead(lexer.GetNextToken());
            int look_ahead_index2 = look_ahead.Count() - 1;
            if (pass(varOption,typesOptions) &&
                (look_ahead[look_ahead_index].type == TokenType.ID
                || look_ahead[look_ahead_index].type == TokenType.PUNT_SQUARE_BRACKET_OPEN
                || look_ahead[look_ahead_index].type == TokenType.PUNT_ACCESOR
                || look_ahead[look_ahead_index].type == TokenType.OP_LESS_THAN) 
                && !literalOptions.Contains(look_ahead[look_ahead_index2].type))
            {
                local_variable_declaration();
            }else if (pass(unaryExpressionOptions.Concat(unaryOperatorOptions).Concat(literalOptions).ToArray()))
            {
                statement_expression_list();
            }
            else
            {
                //EPSILON
            }
        }

        /*optional-statement-expression-list:
            | statement-expression-list
            | EPSILON */
        private void optional_statement_expression_list()
        {
            printIfDebug("optional_statement_expression_list");
            if(pass(unaryOperatorOptions,unaryExpressionOptions,literalOptions))
            {
                statement_expression_list();
            }else{
                //EPSILON
            }
        }

        /*statement-expression-list:
	        | statement-expression statement-expression-list-p */
        private void statement_expression_list()
        {
            printIfDebug("statement_expression_list");
            statement_expression();
            statement_expression_list_p();
        }

        /*statement-expression-list-p:
            | ',' statement-expression statement-expression-list-p
            | EPSILON */
        private void statement_expression_list_p()
        {
            printIfDebug("statement_expression_list_p");
            if(pass(TokenType.PUNT_COMMA))
            {
                statement_expression_list_p();
            }else{
                //EPSILON
            }
        }

        /*foreach-statement:
	        | "foreach" '(' type-or-var identifier "in" expression ')' embedded-statement */
        private void foreach_statement()
        {
            printIfDebug("foreach_statement");
            if(!pass(TokenType.RW_FOREACH))
                throwError("'foreach' reserved word expected");
            consumeToken();
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            consumeToken();
            if(!pass(typesOptions,new TokenType[]{TokenType.RW_VAR}))
                throwError("type-or-var expected");
            type_or_var();
            if(!pass(TokenType.ID))
                throwError("identifier  expected");
            consumeToken();
            if(!pass(TokenType.RW_IN))
                throwError("'in' reserved word expected");
            consumeToken();
            expression();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
            embedded_statement();
        }

        /*selection-statement:
            | if-statement
            | switch-statement */
        private SelectionStatementNode selection_statement()
        {
            printIfDebug("selection_statement");
            if(!pass(TokenType.RW_IF,TokenType.RW_SWITCH))
                throwError("'if' or 'switch' statement expected");
            if(pass(TokenType.RW_IF))
            {
                return if_statement();
            }else if(pass(TokenType.RW_SWITCH))
            {
                return switch_statement();
            }
            return null;
        }

        /*if-statement:
	        | "if" '(' expression ')' embedded-statement optional-else-part */
        private IfStatementNode if_statement()
        {
            printIfDebug("if_statement");
            if(!pass(TokenType.RW_IF))
                throwError("'if' reserved word expected");
            consumeToken();
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            consumeToken();
            var exp = expression();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
            var stmts = embedded_statement();
            var elseBock = optional_else_part();
            return new IfStatementNode(exp,stmts,elseBock);
        }

        /*optional-else-part:
            | else-part
            | EPSILON */
        private ElseStatementNode optional_else_part()
        {
            printIfDebug("optional_else_part");
            if(pass(TokenType.RW_ELSE))
            {
                return else_part();
            }
            return null;
        }

        /*else-part:
	        | "else" embedded-statement	 */
        private ElseStatementNode else_part()
        {
            if(!pass(TokenType.RW_ELSE))
                throwError("'else' reserved word expected");
            consumeToken();
            var stmts = embedded_statement();
            return new ElseStatementNode(stmts);
        }

        /*switch-statement:
	        | "switch" '(' expression ')' '{' optional-switch-section-list '}' */
        private SwitchStatementNode switch_statement()
        {
            printIfDebug("switch_statement");
            if(!pass(TokenType.RW_SWITCH))
                throwError("'switch' reserved word expected");
            consumeToken();
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            consumeToken();
            var exp = expression();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
            if(!pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
                throwError("'{' expected");
            consumeToken();
            var switchSections = optional_switch_section_list();
            if(!pass(TokenType.PUNT_CURLY_BRACKET_CLOSE))
                throwError("'}' expected");
            consumeToken();
            return new SwitchStatementNode(exp,new SwitchBodyNode(switchSections));
        }

        /*optional-switch-section-list:
            | switch-label-list optional-statement-list optional-switch-section-list
            | EPSILON */
        private List<SwitchSectionNode> optional_switch_section_list()
        {
            printIfDebug("optional_switch_section_list");
            if(pass(switchLabelOptions))
            {
                var switchLabels = switch_label_list();
                var stmts = optional_statement_list();
                var switchSection = new SwitchSectionNode(switchLabels,stmts);
                var swicthSections = optional_switch_section_list();
                swicthSections.Insert(0,switchSection);
                return swicthSections;
            }
            return new List<SwitchSectionNode>();
        }

        /*switch-label-list:
	        | switch-label switch-label-list-p */
        private List<CaseNode> switch_label_list()
        {
            printIfDebug("switch_label_list");
            if(!pass(switchLabelOptions))
                throwError("'case' or 'default' expected");
            var caseLabel = switch_label();
            var casesList = switch_label_list_p();
            casesList.Insert(0,caseLabel);
            return casesList;
        }

        /*switch-label-list-p:
            | switch-label-list
            | EPSILON */
        private List<CaseNode> switch_label_list_p()
        {
            printIfDebug("switch_label_list_p");
            if(pass(switchLabelOptions))
            {
                return switch_label_list();
            }else{
                return new List<CaseNode>();
            }
        }

        /*switch-label:
            | "case" expression ':'
            | "default" ':' */
        private CaseNode switch_label()
        {
            printIfDebug("switch_label");
            var caseType = token.type;
            ExpressionNode exp = null;
            if(pass(TokenType.RW_CASE))
            {
                consumeToken();
                exp = expression();
            }else if(pass(TokenType.RW_DEFAULT))
            {
                consumeToken();
            }else{
                throwError("'case' or 'default' expected");
            }
            if(!pass(TokenType.PUNT_COLON))
                throwError("':' expected");
            consumeToken();
            return new CaseNode(caseType,exp);
        }

        /*statement-expression:
	        | unary-expression statement-expression-factorized */
        private EmbeddedStatementNode statement_expression() //TODO
        {
            printIfDebug("statement_expression");
            unary_expression();
            statement_expression_factorized();
            return null;
        }

        /*statement-expression-factorized:
	        | assignment-operator expresion statement-expresion-p
            | statement-expresion-p */
        private void statement_expression_factorized()
        {
            printIfDebug("statement_expression_factorized");
            if (pass(assignmentOperatorOptions))
            {
                consumeToken();
                expression();
                statement_expression_p();
            }
            else
            {
                statement_expression_p();
            }
        }

        /*statement-expression-p:
            | '(' argument-list ')'
            | increment-decrement
            | EPSILON */
        private void statement_expression_p()
        {
            printIfDebug("statement_expression_p");
            if(pass(TokenType.PUNT_PAREN_OPEN))
            {
                consumeToken();
                argument_list();
                if(!pass(TokenType.PUNT_PAREN_CLOSE))
                    throwError(") expected");
                consumeToken();
            }else if(pass(TokenType.OP_PLUS_PLUS,TokenType.OP_MINUS_MINUS))
            {
                consumeToken();
            }else{
                //EPSILON
            }
        }

        /*optional-unary-expression:
            | expression-unary-operator unary-expression
            | '(' type ')'
            | EPSILON */
        private void optional_unary_expression()
        {
            printIfDebug("optional_unary_expression");
            if(pass(TokenType.PUNT_PAREN_OPEN))
            {
                consumeToken();
                if(!pass(typesOptions))
                    throwError("type expected");
                types();
                if(!pass(TokenType.PUNT_PAREN_CLOSE))
                    throwError(") expected");
                consumeToken();
            }else if(pass(unaryOperatorOptions))
            {
                if(!pass(unaryOperatorOptions))
                    throwError("unary operator expected");
                unary_expression();
            }else{
                //EPSILON
            }
        }
    }
}