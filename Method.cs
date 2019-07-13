using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tankLang
{
    // represents a variable type
    public enum builtInType
    {
        STRING,
        INTEGER,
        VOID
    }
    // represents a value
    public class mValue
    {
        private builtInType type;
        private Object value;
        public mValue(builtInType type, Object value)
        {
            this.type = type;
            this.value = value;
        }
        public builtInType getType()
        {
            return type;
        }
        public Object getValue()
        {
            return value;
        }
        public void setValue(Object value)
        {
            this.value = value;
        }
    }
    public class mParameter
    {
        private String name;
        private builtInType type;

        public mParameter(builtInType type, String name)
        {
            this.type = type;
            this.name = name;
        }
        public String getName()
        {
            return name;
        }
        public builtInType getType()
        {
            return type;
        }
    }
    // represents a method
    class Method : Block
    {
        private String name;
        private builtInType type;
        private mParameter[] param;
        private mValue returnValue;
        public Method(Block superBlock, String name, builtInType type, mParameter[] param) : base(superBlock)
        {
            this.name = name;
            this.type = type;
            this.param = param;
        }
        public override void run()
        {
            invoke();
        }
        public mValue invoke(params mValue[] values)
        {
            // invoke the method with the supplied values

            if(values.Length != param.Length)
            {
                throw new ArgumentException("Wrong number of values for paramateres.");
            }

            for(int i = 0; i < values.Length && i < param.Length; i++)
            {
                mParameter p = param[i];
                mValue v = values[i];
                if(p.GetType() != v.GetType())
                {
                    throw new Exception("Parameter " + p.getName() + " should be " + p.getType() + ". Got " + v.getType());
                }
                addVariable(new Variable(this, p.getType(), p.getName(), v.getValue()));
            }

            foreach(Block b in getSubBlocks())
            {
                b.run();

                if(returnValue != null)
                {
                    break;
                }
            }

            if(returnValue == null && type != builtInType.VOID)
            {
                throw new Exception("Expected return value, got none.");
            }

            mValue localReturnValue = returnValue;
            returnValue = null;
            return localReturnValue;
        }
    }
}
