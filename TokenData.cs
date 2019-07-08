using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace tankLang
{
    class TokenData
    {
        private Regex pattern;
        private TokenType type;

        public TokenData(Regex pattern, TokenType type)
        {
            this.pattern = pattern;
            this.type = type;
        }

        public Regex getPattern()
        {
            return pattern;
        }

        public TokenType getType()
        {
            return type;
        }
    }
}
