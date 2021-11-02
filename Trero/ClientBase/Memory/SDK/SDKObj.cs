using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trero.ClientBase.Memory.SDK
{
    public abstract class SDKObj
    {
        public ulong addr;
        public SDKObj(ulong addr) => this.addr = addr;
    }
}
