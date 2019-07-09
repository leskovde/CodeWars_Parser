using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace tankLang
{
    class ClassParser : Parser<Class>
    {
        override public bool shouldParse(String line)
        {
            return Regex.Match(line, "class [a-zA-Z][a-zA-Z0-9]*").Success;
        }
        
        override public Class parse(Block superBlock, Tokenizer tokenizer)
        {
            tokenizer.nextToken(); // skip the class token
            String name = tokenizer.nextToken().getToken(); // get the string value of the next token
            return new Class(name);
        }
    }
}
