using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trero.ClientBase.Memory.SDK;

namespace Trero.ClientBase.Memory
{
    public class Minecraft
    {
        public static ClientInstance CI
        { get => new ClientInstance(MCM.baseEvaluatePointer(0x041FC2A0, new ulong[] { 0x0, 0x50 })); } // clientInstance
    }
}
