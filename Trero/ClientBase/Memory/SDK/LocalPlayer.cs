using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trero.ClientBase.Memory.SDK
{
    public class LocalPlayer : Actor
    {
        public LocalPlayer(ulong addr) : base(addr) { }

        public PlayerAttributes playerAttributes
        { get => new PlayerAttributes(MCM.evaluatePointer(addr + 490, MCM.ceByte2uLong("18 2C0 0"))); }
    }
}
