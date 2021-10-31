#region

using Trero.ClientBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class Freelook : Module
    {
        public Freelook() : base("Freelook", (char)0x07, "Visual", "Freeze your pitch and yaw so you can look around freely")
        {
        }

        public override void OnDisable()
        {
            base.OnDisable();

            OverrideBase.Pitch = true;
            OverrideBase.Yaw = true;
        }

        public override void OnTick()
        {
            base.OnTick();

            OverrideBase.Pitch = false;
            OverrideBase.Yaw = false;
        }
    }
}
