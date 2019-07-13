using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace tankLang
{
    class VariableParser : Parser<Block>
    {
        override public bool shouldParse(String line)
        {
            return Regex.Match(line, "var [a-zA-Z]+[a-zA-Z0-9]+ = \"?[a-zA-Z0-9]\"?").Success;
        }
        override public Block parse(Block superBlock, Tokenizer tokenizer)
        {
            tokenizer.nextToken(); // skip the var token

            Enum.TryParse(tokenizer.nextToken().getToken().ToUpper(), out builtInType type);

            //mType type = mType.valueOf(tokenizer.nextToken().getToken().toUpperCase());

            if(type == builtInType.VOID)
            {
                throw new Exception("Cannot declare variables of type void.");
            }

            String name = tokenizer.nextToken().getToken();
            tokenizer.nextToken(); // skip the = token
            Token v = tokenizer.nextToken();
            Object value = null;

            if (v.getType() == TokenType.INTEGER_LITERAL)
            {
                value = Convert.ToInt32(v.getToken());
            }
            else if (v.getType() == TokenType.STRING_LITERAL)
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
