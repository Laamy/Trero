#region

using Trero.ClientBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class Gamemode : Module
    {
        public Gamemode() : base("Gamemode", (char)0x07, "Exploits", "Sets your gamemode to 0-1 or 2")
        {
            addBypass(new BypassBox(new string[] { "Survival", "Creative", "Adventure" }));
        } // 0x07 = no keybind

        public override void OnEnable()
        {
            base.OnEnable();
            Game.gamemode = bypasses[0].curIndex;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            Game.gamemode = 0;
        }
    }
}