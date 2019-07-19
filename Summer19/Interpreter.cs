using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summer19
{
    public class Interpreter:IVisitor<Object>
    {
        public void interpret (Expr expression)
        {
            try
            {
                Object value = evaluate(expression);
                Console.WriteLine(Stringify(value));
            } catch ( RuntimeError error ) { Console.WriteLine(error.ToString()); }
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
                case TokenType.DIVIDE: { return (int)left / (int)right; }
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

            return "???"; // NOPE
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
            if (unary.opr.type is TokenType.MINUS )
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
            if (expr == null ) { return false; }
            if (expr is Boolean) { return (Boolean)expr; }
            else return true;
        }
    }
}
