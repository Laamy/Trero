using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trero.ClientBase
{
    internal class PlayerModel
    {
        public int playerModelAddr = 0x0;
        public PlayerModel(int addr) => this.playerModelAddr = addr;

        public bool allowArmPositioningX
        {
            set
            {
                if (value)
                {
                    if (MCM.readBaseByte(playerModelAddr) != 0x8B)
                        MCM.writeBaseBytes(playerModelAddr, MCM.ceByte2Bytes("8B 81 10 01 00 00"));
                }
                else
                {
                    if (MCM.readBaseByte(playerModelAddr) == 0x8B)
                        MCM.writeBaseBytes(playerModelAddr, MCM.ceByte2Bytes("90 90 90 90 90 90"));
                }
            }
        }
        public bool visualHandRenderX
        {
            set
            {
                if (value)
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 1)) != 0x89)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 1), MCM.ceByte2Bytes("89 81 EC 00 00 00"));
                }
                else
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 1)) == 0x89)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 1), MCM.ceByte2Bytes("90 90 90 90 90 90"));
                }
            }
        }

        public bool allowArmPositioningY
        {
            set
            {
                if (value)
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 2)) != 0x8B)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 2), MCM.ceByte2Bytes("8B 81 14 01 00 00"));
                }
                else
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 2)) == 0x8B)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 2), MCM.ceByte2Bytes("90 90 90 90 90 90"));
                }
            }
        }
        public bool visualHandRenderY
        {
            set
            {
                if (value)
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 3)) != 0x89)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 3), MCM.ceByte2Bytes("89 81 F0 00 00 00"));
                }
                else
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 3)) == 0x89)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 3), MCM.ceByte2Bytes("90 90 90 90 90 90"));
                }
            }
        }

        public bool allowArmPositioningZ
        {
            set
            {
                if (value)
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 4)) != 0x8B)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 4), MCM.ceByte2Bytes("8B 81 18 01 00 00"));
                }
                else
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 4)) == 0x8B)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 4), MCM.ceByte2Bytes("90 90 90 90 90 90"));
                }
            }
        }
        public bool visualHandRenderZ
        {
            set
            {
                if (value)
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 5)) != 0x89)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 5), MCM.ceByte2Bytes("89 81 F4 00 00 00"));
                }
                else
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 5)) == 0x89)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 5), MCM.ceByte2Bytes("90 90 90 90 90 90"));
                }
            }
        }

        public bool f5RenderX
        {
            set
            {
                if (value)
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 6)) != 0x8B)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 6), MCM.ceByte2Bytes("8B 81 1C 01 00 00"));
                }
                else
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 6)) == 0x8B)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 6), MCM.ceByte2Bytes("90 90 90 90 90 90"));
                }
            }
        }
        public bool bodySpinningX
        {
            set
            {
                if (value)
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 7)) != 0x89)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 7), MCM.ceByte2Bytes("89 81 F8 00 00 00"));
                }
                else
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 7)) == 0x89)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 7), MCM.ceByte2Bytes("90 90 90 90 90 90"));
                }
            }
        }

        public bool f5RenderY
        {
            set
            {
                if (value)
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 8)) != 0x8B)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 8), MCM.ceByte2Bytes("8B 81 20 01 00 00"));
                }
                else
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 8)) == 0x8B)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 8), MCM.ceByte2Bytes("90 90 90 90 90 90"));
                }
            }
        }
        public bool bodySpinningY
        {
            set
            {
                if (value)
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 9)) != 0x89)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 9), MCM.ceByte2Bytes("89 81 FC 00 00 00"));
                }
                else
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 9)) == 0x89)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 9), MCM.ceByte2Bytes("90 90 90 90 90 90"));
                }
            }
        }

        public bool f5RenderZ
        {
            set
            {
                if (value)
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 10)) != 0x8B)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 10), MCM.ceByte2Bytes("8B 81 24 01 00 00"));
                }
                else
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 10)) == 0x8B)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 10), MCM.ceByte2Bytes("90 90 90 90 90 90"));
                }
            }
        }
        public bool bodySpinningZ
        {
            set
            {
                if (value)
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 11)) != 0x89)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 11), MCM.ceByte2Bytes("89 81 00 01 00 00"));
                }
                else
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 11)) == 0x89)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 11), MCM.ceByte2Bytes("90 90 90 90 90 90"));
                }
            }
        }

        public bool allowModelScalingX
        {
            set
            {
                if (value)
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 12)) != 0x8B)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 12), MCM.ceByte2Bytes("8B 81 28 01 00 00"));
                }
                else
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 12)) == 0x8B)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 12), MCM.ceByte2Bytes("90 90 90 90 90 90"));
                }
            }
        }
        public bool _4dScalingX
        {
            set
            {
                if (value)
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 13)) != 0x89)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 13), MCM.ceByte2Bytes("89 81 04 01 00 00"));
                }
                else
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 13)) == 0x89)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 13), MCM.ceByte2Bytes("90 90 90 90 90 90"));
                }
            }
        }

        public bool allowModelScalingY
        {
            set
            {
                if (value)
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 14)) != 0x8B)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 14), MCM.ceByte2Bytes("8B 81 2C 01 00 00"));
                }
                else
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 14)) == 0x8B)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 14), MCM.ceByte2Bytes("90 90 90 90 90 90"));
                }
            }
        }
        public bool _4dScalingY
        {
            set
            {
                if (value)
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 15)) != 0x89)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 15), MCM.ceByte2Bytes("89 81 08 01 00 00"));
                }
                else
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 15)) == 0x89)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 15), MCM.ceByte2Bytes("90 90 90 90 90 90"));
                }
            }
        }

        public bool allowModelScalingZ
        {
            set
            {
                if (value)
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 16)) != 0x8B)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 16), MCM.ceByte2Bytes("8B 81 30 01 00 00"));
                }
                else
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 16)) == 0x8B)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 16), MCM.ceByte2Bytes("90 90 90 90 90 90"));
                }
            }
        }
        public bool _4dScalingZ
        {
            set
            {
                if (value)
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 17)) != 0x89)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 17), MCM.ceByte2Bytes("89 81 0C 01 00 00"));
                }
                else
                {
                    if (MCM.readBaseByte(playerModelAddr + (6 * 17)) == 0x89)
                        MCM.writeBaseBytes(playerModelAddr + (6 * 17), MCM.ceByte2Bytes("90 90 90 90 90 90"));
                }
            }
        }
    } // Class size 0x66
}
