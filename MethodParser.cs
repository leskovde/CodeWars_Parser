using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace tankLang
{
    class MethodParser : Parser<Method>
    {
        override public bool shouldParse(String line)
        {
            //return Regex.Match(line, "method [a-zA-Z][a-zA-Z0-9]* requires \\(([a-zA-Z][a-zA-Z0-9]* [a-zA-Z][a-zA-Z0-9]*)*\\) returns [a-zA-Z][a-zA-Z0-9]*").Success;
            return Regex.Match(line, "method [a-zA-Z][a-zA-Z0-9]* requires \\((.*.)?\\) returns [a-zA-Z][a-zA-Z0-9]*").Success;

        }
        override public Method parse(Block superBlock, Tokenizer tokenizer)
        {
            builtInType myType;
            tokenizer.nextToken(); // skip the method token
            String name = tokenizer.nextToken().getToken();
            tokenizer.nextToken(); // skip the required token
            tokenizer.nextToken(); // skip the ( token

            Token first = tokenizer.nextToken();
            List<mParameter> param = new List<mParameter>();
            if (first.getToken() != ")")
            {
                string[] paramData = new string[] { first.getToken(), null }; // 0 = type, 1 = value
                while (tokenizer.hasNextToken())
                {
                    Token token = tokenizer.nextToken();
                    if(token.getToken() == ")")
                    {
                        break;
                    }
                    if(paramData[0] == null)
                    {
                        paramData[0] = token.getToken();
                    }
                    else
                    {
                        paramData[1] = token.getToken();

                        Enum.TryParse(paramData[0].ToUpper(), out myType);
                        param.Add(new mParameter(myType, paramData[1]));

                        //param.Add(new mParameter(paramData[0].ToUpper(), paramData[1])); // 5 17:00

                        paramData = new string[2];
                    }
                }
            }

            tokenizer.nextToken(); // skip returns token
            Enum.TryParse(tokenizer.nextToken().getToken().ToUpper(), out myType);
            builtInType returnType = myType;
            return new Method(superBlock, name, returnType, param.ToArray());
        }
    }
}
