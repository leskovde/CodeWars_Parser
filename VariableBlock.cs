using System;
using System.Collections.Generic;
using System.Text;

namespace tankLang
{
    class VariableBlock : Block
    {
        private String type, name;
        private Object value;

        public VariableBlock(Block superBlock, String type, String name, Object value) : base(superBlock)
        {
            this.type = type;
            this.name = name;
            this.value = value;
        }

        public String getName()
        {
            return name;
        }

        public override void run()
        {
            if(type.ToUpper() == builtInType.VOID.ToString())
            {
                throw new Exception("Cannot declare variables of type void");
            }

            Enum.TryParse(type, out builtInType myType);
            getSuperBlock().addVariable(new Variable(getSuperBlock(), myType, name, value));
        }
    }
}
