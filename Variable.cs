using System;
using System.Collections.Generic;
using System.Text;

namespace tankLang
{
    class Variable : mValue
    {
        private Block block;
        private String name;

        public Variable(Block block, builtInType type, String name, Object value) : base(type, value)
        {
            this.block = block;
            this.name = name;
        }

        public Block GetBlock()
        {
            return block;
        }
        public String getName()
        {
            return name;
        }
    }
}
