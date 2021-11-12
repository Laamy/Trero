using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trero.ClientBase.VersionBase;

namespace Trero.ClientBase
{
    internal class OverrideBase
    {
        public static PlayerModel entityModel = new PlayerModel(0x1CB4BC0);
        public static bool Pitch
        {
            set
            {
                const int offset = 0x1DE69B2;
                if (value) MCM.writeBaseBytes(offset, MCM.ceByte2Bytes("89 87 38 01 00 00"));
                else MCM.writeBaseBytes(offset, MCM.ceByte2Bytes("90 90 90 90 90 90"));
            }
        }
        public static bool Yaw
        {
            set
            {
                const int offset = 0x1DE69A7;
                if (value) MCM.writeBaseBytes(offset, MCM.ceByte2Bytes("89 01"));
                else MCM.writeBaseBytes(offset, MCM.ceByte2Bytes("90 90"));
            }
        }
        public static bool CanSendPackets
        {
            set
            {
                if (value)
                {
                    if (MCM.readByte(Game.nopacketAddr) != 0x48)
                        MCM.writeBytes(Game.nopacketAddr, MCM.ceByte2Bytes("48 89 5C 24 08"));
                }
                else
                {
                    if (MCM.readByte(Game.nopacketAddr) == 0x48)
                        MCM.writeBytes(Game.nopacketAddr, MCM.ceByte2Bytes("C3 C3 C3 C3 C3"));
                }
            }
        }
        public static bool ServerCanTeleportClient
        {
            /*
            0x1DE9EB9 - F3 0F 11 81 C0 04 00 00
            0x1DE9EC8 - F3 0F 11 A1 CC 04 00 00
            0x1DE9ED4 - F3 0F 11 99 C4 04 00 00

            0x1DE9EDC - F3 0F 11 81 C8 04 00 00
            0x1DE9EEC - F3 0F 11 89 D4 04 00 00
            0x1DE9EF4 - F3 0F 11 99 D0 04 00 00
            90 90 90 90 90 90 90 90
           */
            set
            {
                if (value)
                {
                    MCM.writeBaseBytes(0x1DE9EB9, MCM.ceByte2Bytes("F3 0F 11 81 C0 04 00 00")); // This is only 1/3 pieces for NoLagBack
                    MCM.writeBaseBytes(0x1DE9EC8, MCM.ceByte2Bytes("F3 0F 11 A1 CC 04 00 00"));
                    MCM.writeBaseBytes(0x1DE9ED4, MCM.ceByte2Bytes("F3 0F 11 99 C4 04 00 00"));

                    MCM.writeBaseBytes(0x1DE9EDC, MCM.ceByte2Bytes("F3 0F 11 81 C8 04 00 00"));
                    MCM.writeBaseBytes(0x1DE9EEC, MCM.ceByte2Bytes("F3 0F 11 89 D4 04 00 00"));
                    MCM.writeBaseBytes(0x1DE9EF4, MCM.ceByte2Bytes("F3 0F 11 99 D0 04 00 00"));
                }
                else
                {
                    MCM.writeBaseBytes(0x1DE9EB9, MCM.ceByte2Bytes("90 90 90 90 90 90 90 90"));
                    MCM.writeBaseBytes(0x1DE9EC8, MCM.ceByte2Bytes("90 90 90 90 90 90 90 90"));
                    MCM.writeBaseBytes(0x1DE9ED4, MCM.ceByte2Bytes("90 90 90 90 90 90 90 90"));

                    MCM.writeBaseBytes(0x1DE9EDC, MCM.ceByte2Bytes("90 90 90 90 90 90 90 90"));
                    MCM.writeBaseBytes(0x1DE9EEC, MCM.ceByte2Bytes("90 90 90 90 90 90 90 90"));
                    MCM.writeBaseBytes(0x1DE9EF4, MCM.ceByte2Bytes("90 90 90 90 90 90 90 90"));
                }
            }
        }
        public static bool lookingAtBlock
        {
            set
            {
                //const int offset = 0x96CCE5; //v1.17.41
                //const int offset2 = 0x971C17; //v1.17.41
                ulong labOffset = Game.level + VersionClass.GetData("lookingAtBlock"); // this is a better method!
                if (value)
                {
                    //MCM.writeBaseBytes(offset, MCM.ceByte2Bytes("89 41 18"));
                    //MCM.writeBaseBytes(offset2, MCM.ceByte2Bytes("C7 40 18 03 00 00 00"));
                    MCM.freezeBytes(labOffset, MCM.int2Bytes(0));
                }
                else
                {
                    //MCM.writeBaseBytes(offset, MCM.ceByte2Bytes("90 90 90"));
                    //MCM.writeBaseBytes(offset2, MCM.ceByte2Bytes("90 90 90 90 90 90 90"));
                    MCM.unfreezeBytes(labOffset);
                }
            }
        }
    }
}
