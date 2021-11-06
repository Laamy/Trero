using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trero.ClientBase.Memory.SDK
{
    public class ClientInstance : SDKObj
    {
        public ClientInstance(ulong addr) : base(addr) { }
        public ulong packetFuncAddr => MCM.evaluatePointer(addr + 0xD0, new ulong[] { 0x0, 0x8, 0x0 });

        public LocalPlayer localPlayer => new LocalPlayer(MCM.readInt64(addr + 138));
        public TimerClass timerClass => new TimerClass(MCM.evaluatePointer(addr, MCM.ceByte2uLong("B0 D0")));

        // GuiData

        public bool isIngame() => localPlayer.addr != 0;
        public void SendNotification(string str, int dur, Color colour) { }
    }
}