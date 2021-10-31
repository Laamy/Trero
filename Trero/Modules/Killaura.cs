#region

using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class Killaura : Module
    {
        public Killaura() : base("Killaura", (char)0x07, "Combat", "Attack entities around you automatically")
        {
            addBypass(new BypassBox(new string[] { "Mobaura: False", "Mobaura: True" }));
        }

        public override void OnTick()
        {
            var list = Game.getPlayers();
            if (bypasses[0].curIndex == 1)
                list = Game.getEntites();
            foreach (var ent in list)
                if (Game.position.Distance(ent.position) < 6f)
                    ent.hitbox = Base.Vec2(7f, 7f);
                else ent.hitbox = Base.Vec2(0.6f, 1.8f);

            if (Game.isLookingAtEntity && MCM.isMinecraftFocused())
                Mouse.MouseEvent(Mouse.MouseEventFlags.MOUSEEVENTF_LEFTDOWN);
        }
    }
}