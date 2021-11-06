#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class FastFall : Module
    {
        public AntiImmoblie() : base("FastFall", (char)0x07, "Movement", "Fall Faster - Xello!")
        {
        } // Not defined

        public override void OnEnable()
        {
            MCM.writeBaseBytes(0x23D3A10, MCM.ceByte2Bytes("90 90 90 90 90 90 90")); 
            base.OnEnable();
        }

        public override void OnDisable()
        {
            MCM.writeBaseBytes(0x23D3A10, MCM.ceByte2Bytes("C7 40 1C 00 00 00 00"));
            base.OnDisable();
        }
    }
}