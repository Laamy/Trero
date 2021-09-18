#region

using Trero.ClientBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class Step : Module
    {
        public Step() : base("Step", (char)0x07, "Player")
        {
            addBypass(new BypassBox(new string[] { "Plus 0.5", "Plus 1.5" }));
        } // 0x07 = no keybind

        public override void OnEnable()
        {
            base.OnEnable();
            if (bypasses[0].curIndex == 0)
                Game.stepHeight = 1f;
            if (bypasses[0].curIndex == 1)
                Game.stepHeight = 2f;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            Game.stepHeight = .5f;
        }
    }
}