#region

using Trero.ClientBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class Step : Module
    {
        public Step() : base("Step", (char)0x07, "Player", "Step up full blocks just like its a slab")
        {
            addBypass(new BypassBox(new string[] { "Height: 1f", "Height: 2f" }));
        } // 0x07 = no keybind

        public override void OnEnable()
        {
            base.OnEnable();

            switch (bypasses[0].curIndex)
            {
                case 0:
                    Game.stepHeight = 1f;
                    break;
                case 1:
                    Game.stepHeight = 2f;
                    break;
            }
        }

        public override void OnDisable()
        {
            base.OnDisable();

            Game.stepHeight = .5f;
        }
    }
}