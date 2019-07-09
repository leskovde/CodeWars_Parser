using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace tankLang
{
    class VariableParser<Block> : Parser<Block>
    {
        override public bool shouldParse(String line)
        {
            return Regex.Match(line, "var [a-zA-Z]+[a-zA-Z0-9]+ = \"?[a-zA-Z0-9]\"?").Success;
        }
        override public Block parse(Block superBlock, Tokenizer tokenizer)
        {
            tokenizer.nextToken(); // skip the var token
            mType type = mType.valueOf(tokenizer.nextToken().getToken().toUpperCase());

            String name = tokenizer.nextToken().getToken();
            tokenizer.nextToken(); // skip the = token
            Token value = tokenizer.nextToken();
            Object value;
            
            if(value.getType() == TokenType.INTEGER_LITERAL)
            {
                value = Integer.valueOf(v.getToken());
            }
            else if(v.getType() == TokenType.STRING_LITERAL)
            {
                value = v.getToken();
            }
            else
            {
                // this is an identifier, we need to get its value
                value = superBlock.getVariable(v.getToken()).getValue();
            }
            // add this variable to the block
            superBlock.addVariable(new Variable(superBlock, type, name, value));
            return null;
        }
    }
}
