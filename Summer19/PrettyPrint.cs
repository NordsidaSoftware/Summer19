using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summer19
{
    public class PrettyPrint:IExprVisitor<string>
    {
        public string VisitBinary(Binary binary)
        {
            return Parenthesize(binary.opr.ToString(), binary.lExpr, binary.rExpr);
        }

        public string VisitGrouping(Grouping grouping)
        {
            return Parenthesize("Group", grouping.expr);
        }

        public string VisitLiteral(Literal literal)
        {

           if (literal.token.type is TokenType.WORD)
            {
                Word word = (Word)literal.token;
                return word.value;
            }
           if (literal.token.type is TokenType.NUMBER)
            {
                Number number = (Number)literal.token;
                return number.value.ToString();
            }
            return "???";    // Not a good solution...
        }

        public string VisitUnary(Unary unary)
        {
            return Parenthesize(unary.opr.ToString(), unary.expr);
        }

        private string Parenthesize(string operation, params Expr [] exprs)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("( ");
            sb.Append(operation);
            foreach (Expr expr in exprs)
            {
                sb.Append(expr.Accept(this) + " ");
               
            }
            sb.Append(" )");
            return sb.ToString();
        }

        public string print(Expr expression)
        {
            return expression.Accept(this);
        }
    }
}
