using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summer19
{
    public abstract class Stmt
    {
        public abstract T Accept<T>(IStmtVisitor<T> visitor);
    }

    public class ExpressionStmt : Stmt
    {
        public Expr expression;

        public ExpressionStmt(Expr expression) { this.expression = expression; }
        public override T Accept<T>(IStmtVisitor<T> visitor)
        {
            return visitor.visitExpresstionStmt(this);
        }
    }

    public class PrintStmt : Stmt
    {
        public Expr expression;

        public PrintStmt(Expr expression) { this.expression = expression; }
        public override T Accept<T>(IStmtVisitor<T> visitor)
        {
            return visitor.visitPrintStmt(this);
        }
    }



    // ====================================================
    public interface IStmtVisitor<T>
    {
        T visitExpresstionStmt(ExpressionStmt expressionStmt);
        T visitPrintStmt(PrintStmt printStmt);
    }
}
