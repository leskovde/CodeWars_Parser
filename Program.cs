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


            /* String code = "class Variables \n " +
                          "method main requires () returns void \n " +
                          "var string str = \"Hello world!\" \n" +
                             "method printString requires (string str) returns void "; */

            String code = "class Variables \n" +
                "method main requires () returns void \n " +
                "var string str = getString ()\n" +
                "printString (str)\n" +
                "method printString requires (string str) returns void \n " +
                "print str \n" +
                "method getString requires () returns string\n" +
                "return \"Hello\"";

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
                    Block newBlock = cParser.parse(block, tokenizer);

                    if (newBlock is Class)
                    {
                        classes.Add((Class)newBlock);
                    }
                    else if (newBlock is Method)
                    {
                        block.getBlockTree()[0].addBlock(newBlock);
                    }
                    else
                    {
                        block.addBlock(newBlock);
                    }

                    block = newBlock;
                    success = true;


                    /*block = cParser.parse(block, tokenizer);
                    success = true;
                    if(block is Class)
                    {
                        classes.Add((Class)block);
                    }*/
                }
                else if (mParser.shouldParse(cLine))
                {
                    //block = mParser.parse(block, tokenizer);
                    /*Block newBlock = mParser.parse(block, tokenizer);
                    success = true;
                    block.addBlock(newBlock);
                    block = newBlock;*/

                    Block newBlock = mParser.parse(block, tokenizer);

                    if (newBlock is Class)
                    {
                        classes.Add((Class)block);
                    }
                    else if (newBlock is Method)
                    {
                        block.getBlockTree()[0].addBlock(newBlock);
                    }
                    else
                    {
                        block.addBlock(newBlock);
                    }

                    block = newBlock;
                    success = true;
                }
                else if (vParser.shouldParse(cLine))
                {
                    /*block = vParser.parse(block, tokenizer);
                    success = true;*/

                    Block newBlock = vParser.parse(block, tokenizer);

                    if (newBlock is Class)
                    {
                        classes.Add((Class)block);
                    }
                    else if (newBlock is Method)
                    {
                        block.getBlockTree()[0].addBlock(newBlock);
                    }
                    else
                    {
                        block.addBlock(newBlock);
                    }

                    //block = newBlock;
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

                    Method refMethod = new Method(null, "", builtInType.INTEGER, null);
                    Type refClass = refMethod.GetType();
                    Type testClass = b.GetType();
                    if(refClass.Equals(testClass))
                        Console.WriteLine("Found block of type Method");

                    if(b is Method)
                    {
                        Method method = (Method)b;

                        Console.WriteLine(method.getName());
                        Console.WriteLine(method.getType());
                        Array arr = method.getParameters();
                        foreach(var item in arr)
                        {
                            Console.WriteLine(item.ToString());
                        }
                        Console.WriteLine();

                        if (method.getName() == "main" && method.getType() == builtInType.VOID && method.getParameters().Length == 0)
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
