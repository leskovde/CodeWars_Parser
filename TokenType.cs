using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace tankLang
{
    public enum TokenType
    {
        EMPTY, // nothing
        TOKEN, // for example ( ) = ,
        IDENTIFIER, // first character is a letter, any proceeding characters are letters or numbers
        INTEGER_LITERAL, // a number
        STRING_LITERAL // anything enclosed in double quotes
    }
}
