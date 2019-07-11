using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tankLang
{
    // represents a variable type
    public enum mType
    {
        STRING,
        INTEGER
    }
    // represents a value
    public class mValue
    {
        private mType type;
        private Object value;
        public mValue(mType type, Object value)
        {
            this.type = type;
            this.value = value;
        }
        public mType getType()
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
        private mType type;

        public mParameter(mType type, String name)
        {
            this.type = type;
            this.name = name;
        }
    }
    // represents a method
    class Method : Block
    {
        private String name;
        private mType type;
        private mParameter[] param;
        private mValue returnValue;
        public Method(Block superBlock, String name, mType type, mParameter[] param) : base(superBlock)
        {
            this.name = name;
            this.type = type;
            this.param = param;
        }
        public override void run()
        {
            invoke();
        }
        public void invoke(params mValue[] values)
        {
            // invoke the method with the supplied values
        }
    }
}
