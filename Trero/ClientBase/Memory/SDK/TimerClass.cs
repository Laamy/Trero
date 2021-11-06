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
            set => MCM.writeFloat(MCM.readInt64(addr + 0x0), value);
            get => MCM.readFloat(MCM.readInt64(addr + 0x0));
        }
    }
}
