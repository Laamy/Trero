using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trero.ClientBase.Memory.SDK
{
    public class LoopbackSender : SDKObj
    {
        public LoopbackSender(ulong addr) : base(addr) { }

        public ulong PacketSenderAddr
        {
            get => MCM.evaluatePointer(addr, MCM.ceByte2uLong("0 8 0"));
        }
    }
}
