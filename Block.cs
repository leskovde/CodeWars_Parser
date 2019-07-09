using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tankLang
{
    // represents a block of code
    abstract class Block
    {
        private Block superBlock;
        private List<Block> subBlocks;

        public Block(Block superBlock)
        {
            this.superBlock = superBlock;
            this.subBlocks = new List<Block>();
        }
        public Block getSuperBlock()
        {
            return superBlock;
        }
        public void addBlock(Block block)
        {
            subBlocks.Add(block);
        }
        public abstract void run();
    }
}
