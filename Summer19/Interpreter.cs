using System;
using System.Collections.Generic;

namespace Summer19
{
    public class Interpreter : IExprVisitor<object>, IStmtVisitor<object>
    {

        public void interpret(List<Stmt> statements)
        {
            try
            {
                foreach (Stmt stmt in statements)
                {
                    Object value = execute(stmt);
                }
            }
            catch (RuntimeError error) { Console.WriteLine(error.ToString()); }
        }

        private object execute(Stmt statement)
        {
            return statement.Accept(this);
        }

        private object evaluate(Expr expression)
        {
            return expression.Accept(this);
        }

        private string Stringify(object value)
        {
            return value.ToString();
        }

        public object VisitBinary(Binary binary)
        {
            object left = evaluate(binary.lExpr);
            object right = evaluate(binary.rExpr);
            switch (binary.opr.type)
            {
                case TokenType.MINUS: { return (int)left - (int)right; }
                case TokenType.STAR: { return (int)left * (int)right; }
                case TokenType.DIVIDE:
                    {
                        if ((int) right == 0 ) { throw new RuntimeError("Divide by 0"); }
                        return (int)left / (int)right;
                    }
                case TokenType.PLUS:
                    {
                        if (left is String && right is String) { return (string)left + (string)right; }
                        if (left is int && right is int) { return (int)left + (int)right; }
                        return "???"; //  still No
                    }
                case TokenType.GREATER: { return (int)left > (int)right; }
                case TokenType.LESS: { return (int)left < (int)right; }
                case TokenType.GREATER_EQUAL: { return (int)left >= (int)right; }
                case TokenType.LESS_EQUAL: { return (int)left <= (int)right; }



            }
            throw new RuntimeError("Unreachable code...");
        }

        public object VisitGrouping(Grouping grouping)
        {
            return evaluate(grouping.expr);
        }

        public object VisitLiteral(Literal literal)
        {
            if (literal.token.type is TokenType.NUMBER)
            {
                return ((Number)literal.token).value;
            }
            if (literal.token.type is TokenType.WORD)
            {
                return ((Word)literal.token).value;
            }
            return "???";         // OOPS NO !
        }

        public object VisitUnary(Unary unary)
        {
            Object expr = unary.expr;
            if (unary.opr.type is TokenType.MINUS)
            {
                return -(double)expr;
            }
            if (unary.opr.type is TokenType.NOT)
            {
                return !IsTruthy(expr);
            }

            return "???";    // NOOOO
        }

        private bool IsTruthy(object expr)
        {
            if (expr == null) { return false; }
            if (expr is Boolean) { return (Boolean)expr; }
            else return true;
        }

        public object visitExpresstionStmt(ExpressionStmt expressionStmt)
        {
            evaluate(expressionStmt.expression);
            return null;  
        }

        public object visitPrintStmt(PrintStmt printStmt)
        {
            object value = evaluate(printStmt.expression);
            Console.WriteLine(Stringify(value));
            return null;
        }
    }
}
