using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trero.ClientBase.Memory.SDK
{
    public class ClientInstance : SDKObj
    {
        public ClientInstance(ulong addr) : base(addr) { }

        public TimerClass timerClass // unsure what this class is called :p
        { get => new TimerClass(MCM.evaluatePointer(addr, MCM.ceByte2uLong("B0"))); } // timerClass

        public LoopbackSender loopbackSender
        { get => new LoopbackSender(MCM.evaluatePointer(addr, MCM.ceByte2uLong("D0"))); } // loopbackSender

        public LocalPlayer localPlayer
        { get => new LocalPlayer(MCM.evaluatePointer(addr, MCM.ceByte2uLong("138"))); } // localPlayer

        // GuiData
    }
}