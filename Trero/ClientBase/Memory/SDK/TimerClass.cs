using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trero.ClientBase.Memory.SDK
{
    public class TimerClass : SDKObj
    {
        public TimerClass(ulong addr) : base(addr) { }

        public float timer
        {
            set => MCM.writeFloat(MCM.evaluatePointer(addr, MCM.ceByte2uLong("D0 0")), value);
            get => MCM.readFloat(MCM.evaluatePointer(addr, MCM.ceByte2uLong("D0 0")));
        }
    }
}
