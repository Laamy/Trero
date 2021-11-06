using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trero.ClientBase.Memory.SDK
{
    public class PlayerAttributes : SDKObj // hasn't changed since forever :p
    {
        public PlayerAttributes(ulong addr) : base(addr) { }

        public float speed
        {
            get => MCM.readFloat(addr + 0x9C);
            set => MCM.writeFloat(addr + 0x9C, value);
        }
    }
}
