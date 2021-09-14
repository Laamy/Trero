#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class Gamemode : Module
    {
        public Gamemode() : base("Gamemode", (char)0x07, "Exploits")
        {
        } // 0x07 = no keybind

        public override void OnEnable()
        {
            base.OnEnable();
            Game.gamemode = 1;
        }

        public override void OnDisable()
        {
            base.OnEnable();
            Game.gamemode = 0;
        }
    }
}