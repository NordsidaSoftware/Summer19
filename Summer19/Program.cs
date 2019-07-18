using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
///                        S U M M E R ' 1 9
///                   ===========================
///           En compiler / interpreter for programmeringsspråket Summer.
///           Påbegynt 6/7 -2019. Bygger i stor grad på Summer 18 fra 2018.
///           
/// </summary>
namespace Summer19
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length  > 1 ) { Console.WriteLine("Error! Usage Summer19 <sourcefile>");
                System.Environment.Exit(-1);
            }

            if (args.Length == 0) {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" S U M M E R `19 ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("REPL exit with command `Quit`");
                Console.Write("\n\n\n");
                Console.ForegroundColor = ConsoleColor.White;
                while (true) { Run(Repl()); } }
            else
            {
                if (File.Exists(args[0])) { string source = File.ReadAllText(args[0]); Run(source); }
                else { Console.WriteLine("Error! File <" + args[0] + "> not found."); }

            }
        }

        public static string Repl()
        {
            Console.Write(">>>");
            string input = Console.ReadLine();
            if (input.ToLower().Trim() == "quit" ) { System.Environment.Exit(0); }
            return input;
        }

        public static void Run(string source)
        {
            Lexer lex = new Lexer(source);
            lex.scan();
            Parser pars = new Parser();
            if (!lex.HasErrors)
            {
                Expr expression = pars.parse(lex.Tokens);
                PrettyPrint pp = new PrettyPrint();
                Console.WriteLine(pp.print(expression));
             
            }
            else { PresentErrors(lex); }
        }

        // Trenger nok en bedre error handler.
        private static void PresentErrors(Lexer lex)
        {
            Console.WriteLine("Lexical errors at");
            Console.WriteLine("Line :");
            foreach (Error e in lex.Errors) { Console.WriteLine(e); }
        }
    }
}
