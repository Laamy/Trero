#region

using Trero.ClientBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class HighJump : Module
    {
        public HighJump() : base("HighJump", (char)0x07, "Player")
        {
            addBypass(new BypassBox(new string[] { "Default", "High", "Super Low", "Low" }));
        } // 0x07 = no keybind

        public override void OnTick()
        {
            if (Game.isNull) return;

            if (bypasses[0].curIndex == 0)
                Game.velocity = Base.Vec3(0, 5);
            if (bypasses[0].curIndex == 1)
                Game.velocity = Base.Vec3(0, 7);
            if (bypasses[0].curIndex == 2)
                Game.velocity = Base.Vec3(0, 3);
            if (bypasses[0].curIndex == 3)
                Game.velocity = Base.Vec3(0, 5);
        }
    }
}