#region

using Trero.ClientBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class Freelook : Module
    {
        public Freelook() : base("Freelook", (char)0x07, "Visual")
        {
        }

        public override void OnEnable()
        {
            base.OnEnable();

            OverrideBase.Pitch = false;
            OverrideBase.Yaw = false;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            OverrideBase.Pitch = true;
            OverrideBase.Yaw = true;
        }
    }
}