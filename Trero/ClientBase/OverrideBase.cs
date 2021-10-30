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
                if (value)
                    MCM.writeBaseBytes(0x1DE69B2, MCM.ceByte2Bytes("89 87 38 01 00 00"));
                else
                    MCM.writeBaseBytes(0x1DE69B2, MCM.ceByte2Bytes("90 90 90 90 90 90"));
            }
            get
            {
                if (MCM.readBaseByte(0x1DE69B2) == 89)
                    return true;
                return false;
            }
        }
        public static bool Yaw
        {
            set
            {
                if (value)
                    MCM.writeBaseBytes(0x1DE69A7, MCM.ceByte2Bytes("89 01"));
                else
                    MCM.writeBaseBytes(0x1DE69A7, MCM.ceByte2Bytes("90 90"));
            }
            get
            {
                if (MCM.readBaseByte(0x1DE69A7) == 89)
                    return true;
                return false;
            }
        }
    }
}
