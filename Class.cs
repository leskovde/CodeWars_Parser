using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tankLang
{
    // represents a class
    class Class : Block
    {
        private String name;
        public Class(String name) : base(null)
        {
            this.name = name;
        }

        public String getName()
        {
            return name;
        }

        override public void run()
        {
            // run main method
            foreach (Block b in getSubBlocks())
            {
                if (b is Method)
                {
                    Method method = (Method)b;
                    if (method.getName() == "main" && method.getType() == builtInType.VOID && method.getParameters().Length == 0)
                    {
                        method.run();
                    }
                }
            }
        }
    }
}
