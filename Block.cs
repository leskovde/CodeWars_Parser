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
        private List<Variable> variables;

        public Block(Block superBlock)
        {
            this.superBlock = superBlock;
            this.subBlocks = new List<Block>();
            this.variables = new List<Variable>();
        }
        public List<Block> getBlockTree()
        {
            List<Block> blocks = new List<Block>();

            Block block = this;

            do
            {
                blocks.Add(block);
                block = block.getSuperBlock();
            } while (block != null);

            blocks.Reverse();
            return blocks;
        }
        public Block getSuperBlock()
        {
            return superBlock;
        }
        public Block[] getSubBlocks()
        {
            return subBlocks.ToArray();
        }
        public void addBlock(Block block)
        {
            subBlocks.Add(block);
        }
        public abstract void run();
        public Variable getVariable(String name)
        {
            // check the superBlock first
            foreach (Variable v in variables)
            {
                if (v.getName() == name)
                {
                    return v;
                }
            }
            return null;
        }
        public void addVariable(Variable v)
        {
            variables.Add(v);
        }
    }
}
