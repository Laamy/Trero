using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trero.ClientBase
{
    internal class OverrideBase
    {
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
                if (value) MCM.writeBytes(Game.nopacketAddr, MCM.ceByte2Bytes("48 89 5C 24 08"));
                else MCM.writeBytes(Game.nopacketAddr, MCM.ceByte2Bytes("C3 90 90 90 90"));
            }
        }
        public static bool lookingAtBlock
        {
            set
            {
                const int offset = 0x96CCE5;
                const int offset2 = 0x971C17;
                if (value)
                {
                    MCM.writeBaseBytes(offset, MCM.ceByte2Bytes("89 41 18"));
                    MCM.writeBaseBytes(offset2, MCM.ceByte2Bytes("C7 40 18 03 00 00 00"));
                }
                else
                {
                    MCM.writeBaseBytes(offset, MCM.ceByte2Bytes("90 90 90"));
                    MCM.writeBaseBytes(offset2, MCM.ceByte2Bytes("90 90 90 90 90 90 90"));
                }
            }
        }
    }
}
