using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace tankLang
{
    class MethodParser<T> : Parser<T>
    {
        override public bool shouldParse(String line)
        {
            return Regex.Match(line, "method [a-zA-Z][a-zA-Z0-9]* requires \\(([a-zA-Z][a-zA-Z0-9]* [a-zA-Z][a-zA-Z0-9]*)*\\) returns [a-zA-Z][a-zA-Z0-9]*").Success;

        }
        public Method parse(Block superBlock, Tokenizer tokenizer)
        {
            tokenizer.nextToken(); // skip the method token
            String name = tokenizer.nextToken().getToken();
            tokenizer.nextToken(); // skip the required token
            tokenizer.nextToken(); // skip the ( token

            Token first = tokenizer.nextToken();
            List<mParameter> param = new List<mParameter>();
            if (first.getToken() != ")")
            {
                String[] paramData = new string[] { first, null }; // 0 = type, 1 = value
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
                        param.Add(new mParameter(mType.)); // 5 17:00
                    }
                }
            }
        }
    }
}
