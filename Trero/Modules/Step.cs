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
        } // 0x07 = no keybind

        public override void OnEnable()
        {
            base.OnEnable();

            Game.stepHeight = 2f;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            Game.stepHeight = .5f;
        }
    }
}