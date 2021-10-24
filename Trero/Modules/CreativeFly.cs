#region

using Trero.ClientBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class CreativeFly : Module
    {
        public CreativeFly() : base("CreativeFly", (char)0x07, "Flies")
        {
            addBypass(new BypassBox(new string[] { "IsFlying: True", "IsFlying: False" }));
        } // Not defined

        public override void OnTick()
        {
            Game.isFlying = (bypasses[0].curIndex != 0);
        }

        public override void OnDisable()
        {
            base.OnDisable();

            Game.isFlying = false;
        }
    }
}