#region

using Trero.ClientBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class NoPacket : Module
    {
        public NoPacket() : base("NoPacket", (char)0x07, "World", "Stop sending packets")
        {
        }

        public override void OnEnable()
        {
            base.OnEnable();

            OverrideBase.CanSendPackets = false;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            OverrideBase.CanSendPackets = true;
        }
    }
}