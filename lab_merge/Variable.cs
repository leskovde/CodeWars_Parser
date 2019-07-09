using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tankLang
{
    class Variable : Value
    {
        private Block block;
        private String name;

        public Variable(Block block, mType type, String name, Object value) : base(type, value)
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
