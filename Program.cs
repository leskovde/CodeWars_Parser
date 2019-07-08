using System;
using System.Text.RegularExpressions;

/*
 * Syntax:
 * 
 * 'code' represents code
 * <identifier> - first character is a letter, any proceeding characters are letters or numbers
 * <type> - a primitive type (string, int, boolean, ...)
 * <value> - either an identifier (for a variable) or a literal (1, "Hello", true)
 * 
 * Class Declaration:
 * 'class <identifier>'
 * 
 * Method Declaration:
 * 'method <identifier> requires ([<identifier = type> <identifier = name> ...]) [returns <identifier = type>]'
 * 
 * Method Invocation:
 * '<identifier = name>([<value> ...])'
 * 
 * Return Statement:
 * 'return <value>'
 * 
 * Variable Declaration:
 * 'var <type> <identifier = name> [= <value>]'
 * 
 * Print Statement:
 * 'print <value>'
*/

namespace tankLang
{
    class Program
    {
        static void Main(string[] args)
        {
            String code = "class HelloWorld \n method main requires() \n print \"Hello\"";
            Tokenizer tokenizer = new Tokenizer(code);
            while (tokenizer.hasNextToken())
            {
                Console.WriteLine(tokenizer.nextToken().getToken());
            }
        }
    }
}
