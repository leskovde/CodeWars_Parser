using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tankLang
{
    abstract class Parser<T> where T : Block
    {
        public abstract bool shouldParse(String line);
        // takes a line and chceck if it is for this parser using regex

        public abstract T parse(Block superBlock, Tokenizer tokenizer);
        // take the superBlock and the tokenizer for the line and return a block of this parser's type
    }
}
