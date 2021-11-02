#region

using System;
using Trero.ClientBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class NoPacket : Module
    {
        public NoPacket() : base("NoPacket", (char)0x07, "World", "Stop sending packets")
        {
            addBypass(new BypassBox(new string[] { "Delay: UntilDisabled", "Delay: 5tps", "Delay: 10tps", "Delay: 15tps", "Delay: 20tps", "Delay: 25tps" }));
        }

        public override void OnEnable()
        {
            base.OnEnable();

            OverrideBase.CanSendPackets = false;
            packets = false;
        }

        int a = 0;
        bool packets = false;

        public override void OnTick()
        {
            switch (bypasses[0].curIndex)
            {
                case 1:
                    if (a == 5)
                    {
                        packets = !packets;
                        OverrideBase.CanSendPackets = packets;
                        a = 0;
                    }
                    break;
                case 2:
                    if (a == 10)
                    {
                        packets = !packets;
                        OverrideBase.CanSendPackets = packets;
                        a = 0;
                    }
                    break;
                case 3:
                    if (a == 15)
                    {
                        packets = !packets;
                        OverrideBase.CanSendPackets = packets;
                        a = 0;
                    }
                    break;
                case 4:
                    if (a == 20)
                    {
                        packets = !packets;
                        OverrideBase.CanSendPackets = packets;
                        a = 0;
                    }
                    break;
                case 5:
                    if (a == 25)
                    {
                        packets = !packets;
                        OverrideBase.CanSendPackets = packets;
                        a = 0;
                    }
                    break;
            }
            a++;
            //Console.WriteLine("Sending packets: " + packets);
        }

        public override void OnDisable()
        {
            base.OnDisable();

            OverrideBase.CanSendPackets = true;
            packets = true;
        }
    }
}