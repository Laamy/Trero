#region

using Trero.ClientBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class Gamemode : Module
    {
        public Gamemode() : base("Gamemode", (char)0x07, "Exploits")
        {
            addBypass(new BypassBox(new string[] { "Creative", "Adventure", "Survival" }));
        } // 0x07 = no keybind

        public override void OnEnable()
        {
            base.OnEnable();
            if (bypasses[0].curIndex == 0)
                Game.gamemode = 1;
            if (bypasses[0].curIndex == 1)
                Game.gamemode = 2;
            if (bypasses[0].curIndex == 2)
                Game.gamemode = 0;
        }

        public override void OnDisable()
        {
            base.OnEnable();
            Game.gamemode = 0;
        }
    }
}