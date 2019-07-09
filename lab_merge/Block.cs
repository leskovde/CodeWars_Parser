using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tankLang
{
    class Block
    {
        private List<Variable> variables;

        public Block(Block superBlock)
        {
            this.variables = new List<Variable>();
        }

        public Variable getVariable(String name)
        {
            // check the superBlock first
            foreach(Variable v in variables)
            {
                if(v.getName == name)
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
