#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class PaperMC : Module
    {
        public PaperMC() : base("PaperMC", (char)0x07, "Exploits", "Turn everyone into... Paper..?")
        {
        } // Not defined

        public override void OnEnable()
        {
            MCM.writeBaseBytes(0x1CB4C08, MCM.ceByte2Bytes("90 90 90 90 90 90"));
            base.OnEnable();
        }

        public override void OnDisable()
        {
            MCM.writeBaseBytes(0x1CB4C08, MCM.ceByte2Bytes("8B 81 28 01 00 00")); // 0x1CB4C08 - 8B 81 28 01 00 00 (Nop)
            base.OnDisable();
        }
    }
}