using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace tankLang
{
    class Tokenizer
    {
        private List<TokenData> tokenDatas;

        private String str;
        private Token lastToken;
        private bool pushBack;

        public Tokenizer(String str)
        {
            this.str = str;
            this.tokenDatas = new List<TokenData>();
            tokenDatas.Add(new TokenData(new Regex("^([a-zA-Z][a-zA-Z0-9]*)"), TokenType.IDENTIFIER));
            tokenDatas.Add(new TokenData(new Regex("^((-)?[0-9]+)"), TokenType.INTEGER_LITERAL));
            tokenDatas.Add(new TokenData(new Regex("^(\".*\")"), TokenType.STRING_LITERAL));

            String[] Tokens = { "=", "\\(", "\\)", "\\.", "\\," };
            foreach(String t in Tokens)
            {
                tokenDatas.Add(new TokenData(new Regex("^(" + t + ")"), TokenType.TOKEN));
            }
        }

        public Token nextToken()
        {
            str = str.Trim();

            if(pushBack)
            {
                pushBack = false;
                return lastToken;
            }

            if(str == "")
            {
                return (lastToken = new Token("", TokenType.EMPTY));
            }

            foreach(TokenData data in tokenDatas)
            {
                Match matcher = data.getPattern().Match(str);
                if (matcher.Success)
                {
                    String token = matcher.Value.Trim();
                    // replaceFirst is missing from C#
                    //var regex = new Regex(Regex.Escape("o"));
                    //str = regex.Replace(str, "", 1);
                    str = data.getPattern().Replace(str, "", 1);

                    if (data.getType() == TokenType.STRING_LITERAL)
                        return (lastToken = new Token(token.Substring(1, token.Length - 2), TokenType.STRING_LITERAL));
                    else
                        return (lastToken = new Token(token, data.getType()));
                }
            }
            throw new Exception("Could not parse " + str);
        }
        public bool hasNextToken()
        {
            if (str == "")
            {
                return false;
            }
            return true;
        }
        // original was pushBack()
        public void PushBack()
        {
            if (lastToken != null)
            {
                this.pushBack = true;
            }
        }
    }
}
