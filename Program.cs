using System;
using System.Collections.Generic;
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
 * 'method <identifier = name> requires ([<identifier = type> <identifier = name> ...]) returns <identifier = type>'
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
            List<Class> classes = new List<Class>();
            Class main = null;
            Block block = null;
            bool success;


            String code = "class HelloWorld \n method main requires () returns void";
            /*while (tokenizer.hasNextToken())
            {
                Console.WriteLine(tokenizer.nextToken().getToken());
            }*/
            //Parser<Method>[] parsers = { new ClassParser(), new MethodParser(), new VariableParser() };
            ClassParser cParser = new ClassParser();
            MethodParser mParser = new MethodParser();
            VariableParser vParser = new VariableParser();

            foreach (String line in code.Split("\n"))
            {
                success = false;
                string cLine = line.Trim();
                Tokenizer tokenizer = new Tokenizer(cLine);
                
                if(cParser.shouldParse(cLine))
                {
                    block = cParser.parse(block, tokenizer);
                    success = true;
                    if(block is Class)
                    {
                        classes.Add((Class)block);
                    }
                }
                else if (mParser.shouldParse(cLine))
                {
                    //block = mParser.parse(block, tokenizer);
                    Block newBlock = mParser.parse(block, tokenizer);
                    success = true;
                    block.addBlock(newBlock);
                    block = newBlock;

                }
                else if (vParser.shouldParse(cLine))
                {
                    block = vParser.parse(block, tokenizer);
                    success = true;
                }

                if(!success)
                {
                    throw new ArgumentException("Invalid line " + cLine);
                }
            }
            foreach(Class c in classes)
            {
                foreach(Block b in c.getSubBlocks())
                {
                    if(b is Method)
                    {
                        Method method = (Method)b;
                        if(method.getName() == "main" && method.getType() == builtInType.VOID && method.getParameters().Length == 0)
                        {
                            main = c;
                        }
                    }
                }
            }
            if (main == null)
            {
                throw new Exception("No main method.");
            }
            else
            {
                Console.WriteLine("Found method.");
            }

            main.run();

            Console.WriteLine("Main class is named " + main.getName());

            Console.ReadLine();
        }
    }
}
