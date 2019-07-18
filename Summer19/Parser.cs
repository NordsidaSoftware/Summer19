using System;
using System.Collections.Generic;

namespace Summer19
{
    /// <summary>
    ///  Grammar for Summer19
    ///  From Bob Nystroms brilliant book.
    ///  10/7-19 : Periander med bihulebetennelse, strålende sol. Sitter i skyggen på Linge.
    ///  =====================================================================================
    ///  An ambiguous grammar :
    /// expr -> literal
    ///        | unary
    ///        | binary
    ///        | grouping
    /// 
    /// literal  -> NUMBER | STRING | "true" | "false" | "nil" ;
    /// unary    -> ("-" | "!" ) expression ;
    /// binary   -> expr operator expression ;
    /// grouping -> "(" expr ")";
    /// 
    /// operator -> "==" | "!=" | "<" | ">" | ">=" | "<=" 
    ///            |"+" | "-" | "*" | "/" ;
    ///            
    /// ------------------------------------------------------------------------------
    /// 14.07.19 : Kjørt hjemmover. Gikk nesten bra.
    /// 
    /// The same grammar, rewritten for precedens and associativity:
    /// 
    /// expression     → equality ;
    /// equality       -> comparison (( "!=" | "==") comparison)* ;
    /// comparison     -> addition (( ">" | ">=" | "<" | "<=") addition)* ;
    /// addition       -> multiplication (( "-" | "+" ) multiplication)* ;
    /// multiplikation -> unary(("/" | "*" ) unary )*;
    /// unary          -> ("!" | "-" ) unary;
    ///                 |  primary;
    /// primary        -> NUMBER | STRING | "false" | "true" | "nil"
    ///                 | "(" expr ")"; 

    /// ==============================================================================
    /// </summary>

    internal class Parser
    {
        int index;
        List<Token> Tokens;

        bool EOF { get { return Tokens[index].type == TokenType.EOF; } }
        Token previous { get { return Tokens[index - 1]; } }

        
        internal Expr parse(List<Token> Tokens)
        {
            this.Tokens = Tokens;
            return expression();

            #region VP TEST CODE
            /*
            ================== VISITOR PATTERN TEST CODE ===============
            Console.WriteLine("Visitor pattern test :");
            Apple a = new Apple();
            Cherry b = new Cherry();
            a.Eat();
            b.Spit();
            MakePai ff = new MakePai();
            GetValue gv = new GetValue();
            Console.WriteLine(a.Accept(ff));
            Console.WriteLine(b.Accept(gv) + a.Accept(gv));
            ============================================================
            */
            #endregion
        }

        void Advance() { if (!EOF) index++; }

        Expr expression()
        {
            return equality();
        }

        Expr equality()
        {
            Expr expr = comparison();
            while (match(TokenType.EQUAL_EQUAL, TokenType.NOT_EQUAL))
            {
                Token opr = previous;
                Expr right = comparison();
                expr = new Binary(expr, opr, right);
            }
            return expr;
        }

        Expr comparison()
        {
            Expr expr = addition();
            while (match(TokenType.GREATER, TokenType.GREATER_EQUAL, TokenType.LESS, TokenType.LESS_EQUAL))
            {
                Token opr = previous;
                Expr right = addition();
                expr = new Binary(expr, opr, right);
            }
            return expr;
        }

        Expr addition()
        {
            Expr expr = multiplication();
            while (match(TokenType.PLUS, TokenType.MINUS))
            {
                Token opr = previous;
                Expr right = multiplication();
                expr = new Binary(expr, opr, right);
            }
            return expr;
        }

        Expr multiplication()
        {
            Expr expr = unary();
            while (match(TokenType.STAR, TokenType.DIVIDE))
            {
                Token opr = previous;
                Expr right = unary();
                expr = new Binary(expr, opr, right);
            }
            return expr;
        }

        Expr unary()
        {
            if (match(TokenType.NOT, TokenType.MINUS))
            {
                Token opr = previous;
                Expr right = unary();
                return new Unary(opr, right);
            }
            return primary();
        }

        Expr primary()
        {
            if (match(TokenType.LPAREN))
            {
                Expr expr = expression();
                if (!Expect(TokenType.RPAREN)) { Console.WriteLine("ERROR"); }
                return new Grouping(expr);
            }
            if (match(TokenType.WORD, TokenType.NUMBER))
            {
                return new Literal(previous);
            }

            return new Literal(new Token(TokenType.SEMICOLON));  // NOO
        }

        private bool Expect(TokenType type)
        {
            if (check(type)) { Advance(); return true; }
            return false;
        }

        private bool check (TokenType type )
        {
            return (Tokens[index].type == type);
        }

        bool match(params TokenType[] types)
        {
            foreach (TokenType type in types)
            {
                if (check(type)) { Advance(); return true; }
            }
            return false;
        }
    }





}