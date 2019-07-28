namespace Summer19
{
    // 12.07.2019 : fint vær, jeg har blitt 43. Går gamle stier på nytt.

    //  ============ EXPRESSIONS ===============
    //                base class
    public abstract class Expr
    {
        public abstract T Accept<T>(IExprVisitor<T> visitor);
    }

    public class Literal : Expr
    {
        public Token token;

        public Literal (Token token) { this.token = token; }
        public override T Accept<T>(IExprVisitor<T> visitor)
        {
            return visitor.VisitLiteral(this);
        }
    }

    public class Unary : Expr
    {
        public Token opr;
        public Expr expr;

        public Unary (Token opr, Expr expr) { this.opr = opr; this.expr = expr; }
        
        public override T Accept<T>(IExprVisitor<T> visitor)
        {
            return visitor.VisitUnary(this);
        }
    }

    public class Binary : Expr
    {
        public Token opr;
        public Expr lExpr;
        public Expr rExpr;

        public Binary(Expr lExpr, Token opr, Expr rExpr)
        { this.lExpr = lExpr; this.opr = opr; this.rExpr = rExpr; }
        public override T Accept<T>(IExprVisitor<T> visitor)
        {
            return visitor.VisitBinary(this);
        }
    }

    public class Grouping : Expr
    {
        public Expr expr;
        public Grouping (Expr expr) { this.expr = expr; }

        public override T Accept<T>(IExprVisitor<T> visitor)
        {
            return visitor.VisitGrouping(this);
        }
    }
    // 
    // ================= END EXPR ==============================
    //
    public interface IExprVisitor<T>
    {
        T VisitLiteral(Literal literal);
        T VisitUnary(Unary unary);
        T VisitBinary(Binary binary);
        T VisitGrouping(Grouping grouping);
    }
}
