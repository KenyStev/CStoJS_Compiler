using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.TreeNodes;
using Compiler.TreeNodes.Expressions;
using Compiler.TreeNodes.Statements;
using Compiler.TreeNodes.Expressions.UnaryExpressions;

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
        private EmbeddedStatementNode embedded_statement()
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
            }else if(pass(iteratorsOptionsStatements))
            {
                return iteration_statement();
            }else if(pass(jumpsOptionsStatements))
            {
                var jumpStmt = jump_statement();
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
                var blockToken = token;
                consumeToken();
                var statements = optional_statement_list();
                if(!pass(TokenType.PUNT_CURLY_BRACKET_CLOSE))
                    throwError("'}' expected");
                consumeToken();
                return new StatementBlockNode(statements,blockToken);
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
        private JumpStatementNode jump_statement()
        {
            printIfDebug("jump_statement");
            if(!pass(jumpsOptionsStatements))
                throwError("break, continue or return expected");
            Token old = token;
            consumeToken();
            ExpressionNode exp=null;
            if(old.type==TokenType.RW_RETURN)
                exp = optional_expression();
            return new JumpStatementNode(old.type,exp,old);
        }

        /*optional-expression: 
            | expression
            | EPSILON */
        private ExpressionNode optional_expression()
        {
            printIfDebug("optional_expression");
            if(pass(expressionOptions()))
            {
                return expression();
            }else{
                return null;
            }
        }

        /*iteration-statement:
            | while-statement
            | do-statement
            | for-statement
            | foreach-statement */
        private IterationStatementNode iteration_statement()
        {
            printIfDebug("iteration_statement");
            if(!pass(iteratorsOptionsStatements))
                throwError("while, do-while, for or foreach conditional statement expected");
            
            if(pass(TokenType.RW_WHILE))
            {
                return while_statement();
            }else if(pass(TokenType.RW_DO))
            {
                return do_statement();
            }else if(pass(TokenType.RW_FOR))
            {
                return for_statement();
            }else if(pass(TokenType.RW_FOREACH))
            {
                return foreach_statement();
            }
            return null;
        }

        /*while-statement:
	        | "while" '(' expression ')' embedded-statement */
        private WhileStatementNode while_statement()
        {
            printIfDebug("while_statement");
            if(!pass(TokenType.RW_WHILE))
                throwError("'while' reserved word expected");
            var whileToken = token;
            consumeToken();
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            consumeToken();
            var exp = expression();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
            var body = embedded_statement();
            return new WhileStatementNode(exp,body,whileToken);
        }

        /*do-statement: 
	        | "do" embedded-statement "while" '(' expression ')' ';' */
        private DoWhileStatementNode do_statement()
        {
            printIfDebug("do_statement");
            if(!pass(TokenType.RW_DO))
                throwError("'do' reserved word expected");
            var doToken = token;
            consumeToken();
            var body = embedded_statement();
            if(!pass(TokenType.RW_WHILE))
                throwError("'while' reserved word expected");
            consumeToken();
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            consumeToken();
            var exp = expression();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
            if(!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                throwError("; expected");
            consumeToken();
            return new DoWhileStatementNode(exp,body,doToken);
        }

        /*for-statement:
	        | "for" '(' optional-for-initializer ';' optional-expression ';' optional-statement-expression-list ')' embedded-statement */
        private ForStatementNode for_statement()
        {
            printIfDebug("for_statement");
            if(!pass(TokenType.RW_FOR))
                throwError("'for' reserved word expected");
            var forToken = token;
            consumeToken();
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            consumeToken();
            //initializer
            var forInitializer = optional_for_initializer();
            if(!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                throwError("';' expected");
            consumeToken();
            //expresion
            var exp = optional_expression();
            if(!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                throwError("';' expected");
            consumeToken();
            //statementlist
            var postIncrementStmts = optional_statement_expression_list();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
            var stmts = embedded_statement();
            return new ForStatementNode(forInitializer,exp,postIncrementStmts,stmts,forToken);
        }

        /*optional-for-initializer:
            | local-variable-declaration
            | statement-expression-list
            | EPSILON */
        private ForInitializerNode optional_for_initializer()
        {
            printIfDebug("optional_for_initializer");
            var forInitialation = token;
            while (pass(typesOptions,varOption))
            {
                addLookAhead(lexer.GetNextToken());
                if (look_ahead[look_ahead.Count() - 1].type == TokenType.PUNT_ACCESOR)
                {
                    addLookAhead(lexer.GetNextToken());
                }
                else
                    break;
            }
            int index;
            int index2 = 0;
            Token placeholder = token;
            if (pass(typesOptions,varOption))
            {
                index = look_ahead.Count() - 1;
                placeholder = look_ahead[index];
                addLookAhead(lexer.GetNextToken());
                index2 = look_ahead.Count() - 1;
            }
            if (
                (pass(typesOptions,varOption) &&
                (placeholder.type == TokenType.ID
                || placeholder.type == TokenType.OP_LESS_THAN
                ||
                (placeholder.type == TokenType.PUNT_SQUARE_BRACKET_OPEN
                && (look_ahead[index2].type == TokenType.PUNT_SQUARE_BRACKET_CLOSE
                || look_ahead[index2].type == TokenType.PUNT_COMMA))))
                )
            {
                var localVariables = local_variable_declaration();
                return new ForInitializerNode(localVariables,forInitialation);
            }else if(pass(unaryExpressionOptions,unaryOperatorOptions,literalOptions))
            {
                var stmtsExpList = statement_expression_list();
                var initialization = new ForInitializerNode(stmtsExpList,forInitialation);
                return initialization;
            }else{
                return null;
            }
        }

        /*optional-statement-expression-list:
            | statement-expression-list
            | EPSILON */
        private List<StatementExpressionNode> optional_statement_expression_list()
        {
            printIfDebug("optional_statement_expression_list");
            if(pass(unaryOperatorOptions,unaryExpressionOptions,literalOptions))
            {
                return statement_expression_list();
            }else{
                return null;
            }
        }

        /*statement-expression-list:
	        | statement-expression statement-expression-list-p */
        private List<StatementExpressionNode> statement_expression_list()
        {
            printIfDebug("statement_expression_list");
            var stmtExp = statement_expression();
            var stmtsExp = statement_expression_list_p();
            stmtsExp.Insert(0,stmtExp);
            return stmtsExp;
        }

        /*statement-expression-list-p:
            | ',' statement-expression statement-expression-list-p
            | EPSILON */
        private List<StatementExpressionNode> statement_expression_list_p()
        {
            printIfDebug("statement_expression_list_p");
            if(pass(TokenType.PUNT_COMMA))
            {
                return statement_expression_list_p();
            }else{
                return new List<StatementExpressionNode>();
            }
        }

        /*foreach-statement:
	        | "foreach" '(' type-or-var identifier "in" expression ')' embedded-statement */
        private ForeachStatementNode foreach_statement()
        {
            printIfDebug("foreach_statement");
            if(!pass(TokenType.RW_FOREACH))
                throwError("'foreach' reserved word expected");
            var foreachToken = token;
            consumeToken();
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            consumeToken();
            if(!pass(typesOptions,new TokenType[]{TokenType.RW_VAR}))
                throwError("type-or-var expected");
            var type = type_or_var();
            if(!pass(TokenType.ID))
                throwError("identifier  expected");
            var identifier = new IdNode(token.lexeme,token);
            consumeToken();
            if(!pass(TokenType.RW_IN))
                throwError("'in' reserved word expected");
            consumeToken();
            var exp = expression();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
            var body = embedded_statement();
            return new ForeachStatementNode(type, identifier, exp, body,foreachToken);
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
            var ifToken = token;
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
            return new IfStatementNode(exp,stmts,elseBock,ifToken);
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
            var elseToken = token;
            consumeToken();
            var stmts = embedded_statement();
            return new ElseStatementNode(stmts,elseToken);
        }

        /*switch-statement:
	        | "switch" '(' expression ')' '{' optional-switch-section-list '}' */
        private SwitchStatementNode switch_statement()
        {
            printIfDebug("switch_statement");
            if(!pass(TokenType.RW_SWITCH))
                throwError("'switch' reserved word expected");
            var switchToken = token;
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
            var switchSectionsToken = token;
            var switchSections = optional_switch_section_list();
            if(!pass(TokenType.PUNT_CURLY_BRACKET_CLOSE))
                throwError("'}' expected");
            consumeToken();
            return new SwitchStatementNode(exp,new SwitchBodyNode(switchSections,switchSectionsToken),switchToken);
        }

        /*optional-switch-section-list:
            | switch-label-list optional-statement-list optional-switch-section-list
            | EPSILON */
        private List<SwitchSectionNode> optional_switch_section_list()
        {
            printIfDebug("optional_switch_section_list");
            if(pass(switchLabelOptions))
            {
                var switchLabelToken = token;
                var switchLabels = switch_label_list();
                var stmts = optional_statement_list();
                var switchSection = new SwitchSectionNode(switchLabels,stmts,switchLabelToken);
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
            var caseToken = token;
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
            return new CaseNode(caseType,exp,caseToken);
        }

        /*statement-expression:
	        | unary-expression statement-expression-factorized */
        private StatementExpressionNode statement_expression()
        {
            printIfDebug("statement_expression");
            var stmtToken = token;
            var unary = unary_expression();
            return new StatementExpressionNode(statement_expression_factorized(unary),stmtToken);
        }

        /*statement-expression-factorized:
	        | assignment-operator expresion statement-expresion-p
            | statement-expresion-p */
        private ExpressionNode statement_expression_factorized(UnaryExpressionNode leftValue)
        {
            printIfDebug("statement_expression_factorized");
            if (pass(assignmentOperatorOptions))
            {
                var assignmentOperator = token;
                consumeToken();
                var exp = expression();
                // return statement_expression_p(new AssignExpressionNode(leftValue,assignmentOperator.type,exp));
                return new AssignExpressionNode(leftValue,assignmentOperator.type,exp,assignmentOperator);
            }
            else
            {
                // return statement_expression_p(leftValue);
                return leftValue;
            }
        }

        // /*statement-expression-p:
        //     | '(' argument-list ')'
        //     | increment-decrement
        //     | EPSILON */
        // private ExpressionNode statement_expression_p(ExpressionNode leftValue)
        // {
        //     printIfDebug("statement_expression_p");
        //     if(pass(TokenType.PUNT_PAREN_OPEN))
        //     {
        //         consumeToken();
        //         var arguments = argument_list();
        //         if(!pass(TokenType.PUNT_PAREN_CLOSE))
        //             throwError(") expected");
        //         consumeToken();
        //     }else if(pass(TokenType.OP_PLUS_PLUS,TokenType.OP_MINUS_MINUS))
        //     {
        //         var operatorUnary = token;
        //         consumeToken();
                
        //     }
        //     return leftValue;
        // }

        // /*optional-unary-expression:
        //     | expression-unary-operator unary-expression
        //     | '(' type ')'
        //     | EPSILON */
        // private void optional_unary_expression()
        // {
        //     printIfDebug("optional_unary_expression");
        //     if(pass(TokenType.PUNT_PAREN_OPEN))
        //     {
        //         consumeToken();
        //         if(!pass(typesOptions))
        //             throwError("type expected");
        //         types();
        //         if(!pass(TokenType.PUNT_PAREN_CLOSE))
        //             throwError(") expected");
        //         consumeToken();
        //     }else if(pass(unaryOperatorOptions))
        //     {
        //         if(!pass(unaryOperatorOptions))
        //             throwError("unary operator expected");
        //         unary_expression();
        //     }else{
        //         //EPSILON
        //     }
        // }
    }
}