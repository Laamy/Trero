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
            Vector2 bRots = Game.bodyRots;
            bRots.y = Game.bodyRots.y;
            bRots.x = Game.bodyRots.x;
            Game.bodyRots = bRots;
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
