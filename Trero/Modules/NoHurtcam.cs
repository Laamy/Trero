#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class NoHurtcam : Module
    {
        public NoHurtcam() : base("NoHurtcam", (char)0x07, "Visual", "Disable Hurttime - Xello!")
        {
        } // Not defined

        public override void OnEnable()
        {
            MCM.writeBaseBytes(0x1EC82F1, MCM.ceByte2Bytes("90 90 90 90 90 90 90 90 90 90")); 
            base.OnEnable();
        }

        public override void OnDisable()
        {
            MCM.writeBaseBytes(0x1EC82F1, MCM.ceByte2Bytes("C7 81 58 07 00 00 0A 00 00 00")); 
            base.OnDisable(); // bit high dm if i fucked it up
        }
    }
}