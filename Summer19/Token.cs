namespace Summer19
{

    public enum TokenType {  WORD,
        NUMBER,
        SEMICOLON,
        PLUS,
        MINUS,
        STAR,
        DIVIDE,
        LPAREN,
        RPAREN,
        APOSTROPHE,
        NOT_EQUAL,
        EQUAL,
        EQUAL_EQUAL,
        NOT,
        GREATER,
        GREATER_EQUAL,
        LESS,
        LESS_EQUAL,
        EOF,
        PRINT
    }
    public class Token
    {
        public TokenType type;
        public Token(TokenType type) { this.type = type; }
        public override string ToString()
        {
            return "[" + type.ToString() + "]";
        }
    }

    public class Word : Token
    {
        public string value;
        public Word(string value):base (TokenType.WORD)
        {
            this.value = value;
        }
        public override string ToString()
        {
            return base.ToString() + " " + value;
        }
    }

    public class Number : Token
    {
        public int value;
        public Number (int value ) : base (TokenType.NUMBER)
        {
            this.value = value;
        }
        public override string ToString()
        {
            return base.ToString() + " " + value.ToString();
        }
    }
}
