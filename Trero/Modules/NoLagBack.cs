#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class NoLagBack : Module
    {
        public NoLagBack() : base("NoLagBack", (char)0x07, "Exploits")
        {
        } // Not defined

        public override void OnEnable()
        {
            base.OnEnable();

            MCM.writeBaseBytes(0x1D65729, MCM.ceByte2Bytes("90 90 90 90 90 90 90 90"));
            MCM.writeBaseBytes(0x1D65738, MCM.ceByte2Bytes("90 90 90 90 90 90 90 90"));
            MCM.writeBaseBytes(0x1D65744, MCM.ceByte2Bytes("90 90 90 90 90 90 90 90"));
            MCM.writeBaseBytes(0x1D6574C, MCM.ceByte2Bytes("90 90 90 90 90 90 90 90"));
            MCM.writeBaseBytes(0x1D6575C, MCM.ceByte2Bytes("90 90 90 90 90 90 90 90"));
            MCM.writeBaseBytes(0x1D65764, MCM.ceByte2Bytes("90 90 90 90 90 90 90 90"));
        }
        public override void OnDisable() // restore code
        {
            base.OnDisable();

            MCM.writeBaseBytes(0x1D65729, MCM.ceByte2Bytes("F3 0F 11 81 C0 04 00 00"));
            MCM.writeBaseBytes(0x1D65729, MCM.ceByte2Bytes("F3 0F 11 A1 CC 04 00 00"));
            MCM.writeBaseBytes(0x1D65729, MCM.ceByte2Bytes("F3 0F 11 99 C4 04 00 00"));
            MCM.writeBaseBytes(0x1D65729, MCM.ceByte2Bytes("F3 0F 11 81 C8 04 00 00"));
            MCM.writeBaseBytes(0x1D65729, MCM.ceByte2Bytes("F3 0F 11 89 D4 04 00 00"));
            MCM.writeBaseBytes(0x1D65729, MCM.ceByte2Bytes("F3 0F 11 99 D0 04 00 00"));
        }
    }
}