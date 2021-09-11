using Trero.ClientBase;

namespace Trero.Modules
{
    class Gamemode : Module
    {
        public Gamemode() : base("Gamemode", (char)0x07, "Exploits") { } // 0x07 = no keybind
        public override void onEnable()
        {
            base.onEnable();
            Game.gamemode = 1;
        }
        public override void onDisable ()
        {
            base.onEnable();
            Game.gamemode = 0;
        }
    }
}
