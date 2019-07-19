using System.Collections.Generic;
using System.Text;

namespace Summer19
{
    /// <summary>
    ///                           L E X E R
    ///                     ====================
    ///         Summer '19 sin lexer er "velskrevet" Laget i løpet av 2 kvelder.
    ///         Fungerer bra så langt. Trenger en error handler modul for å rapportere
    ///         lexical errors
    ///                       
    /// </summary>

    internal class Lexer
    {
        private readonly string source;
        public List<Token> Tokens;
        public List<Error> Errors;
        private int index;
        private char currentChar;
        private int line;

        public bool EOF { get { return index > source.Length - 1; } }
        public bool HasErrors { get { return Errors.Count > 0; } }

        public bool Match(char match)
        {
            if (peek() == match)
            {
                Advance();
                ReadChar();
                return true;
            }
            else return false;
        }

        public char peek() { return source[index + 1]; }   // EOF AND EOF -1 ?
        public void Advance() { index++; }
        public void StepBack() { index--; }

        public void ReadChar() { if (!EOF) currentChar = source[index]; }

        public Lexer(string source)
        {
            this.source = source;
            Tokens = new List<Token>();
            Errors = new List<Error>();
            index = -1;
            line = 1;
        }
        public void scan()
        {
            Errors.Clear();

            while (true)       // ======= START OF SCANNER =======
            {
                Advance();
                ReadChar();
                if ( EOF ) { break; }   //   === BREAK ON EOF ===

                if (currentChar == ' ') RecordWhiteSpace();
                if (currentChar == '\n') { line++; continue; }

                switch (currentChar)
                {
                    //   ====  CALCULATION OPERATORS  ====
                    case '+': { Tokens.Add(new Token(TokenType.PLUS)); break; }
                    case '-': { Tokens.Add(new Token(TokenType.MINUS)); break; }
                    case '*': { Tokens.Add(new Token(TokenType.STAR)); break; }
                    case '/': { Tokens.Add(new Token(TokenType.DIVIDE)); break; }
                    //   ====   OTHER !  ====
                    case '(': { Tokens.Add(new Token(TokenType.LPAREN)); break; }
                    case ')': { Tokens.Add(new Token(TokenType.RPAREN)); break; }
                    case '"': { Tokens.Add(new Token(TokenType.APOSTROPHE)); break; }
                    case ';': { Tokens.Add(new Token(TokenType.SEMICOLON)); break; }
                    case '<': { Tokens.Add(new Token(TokenType.LESS)); break; }
                    case '=': { Tokens.Add(new Token(Match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL)); break; }
                    case '!': { Tokens.Add(new Token(Match('=') ? TokenType.NOT_EQUAL : TokenType.NOT)); break; }
                }

                if (char.IsLetter(currentChar)) { Tokens.Add(RecordWord()); }
                if (char.IsNumber(currentChar)) { Tokens.Add(RecordNumber()); }
  
            }

            Tokens.Add(new Token(TokenType.EOF));
        }

        public void RecordWhiteSpace()
        {
            while (char.IsWhiteSpace(currentChar))
            {
                Advance();
                ReadChar();
                if (EOF) { break; }
            }
        }

        public Token RecordWord()
        {
            StringBuilder sb = new StringBuilder();
            while (char.IsLetter(currentChar))
            {
                sb.Append(currentChar);
                Advance();
                ReadChar();
                if (EOF) { break; }
            }
            StepBack();
            return new Word(sb.ToString());
        }

        public Token RecordNumber()
        {
            StringBuilder sb = new StringBuilder();
            while (char.IsNumber(currentChar))
            {
                sb.Append(currentChar);
                Advance();
                ReadChar();
                if (EOF) { break; }
            }
            int number;
            int.TryParse(sb.ToString(), out number);
            StepBack();
            return new Number(number);
        }
    }
}