#region

using Trero.ClientBase;
using Trero.ClientBase.KeyBase;

#endregion

namespace Trero.Modules
{
    internal class Killaura : Module
    {
        public Killaura() : base("Killaura", (char)0x07, "Combat")
        {
        }

        public override void OnTick()
        {
            foreach (var ent in Game.getPlayers())
                if (Game.position.Distance(ent.position) < 6f)
                    ent.hitbox = Base.Vec2(7f, 7f);
                else ent.hitbox = Base.Vec2(0.6f, 1.8f);

            if (Game.isLookingAtEntity && MCM.isMinecraftFocused())
                Mouse.MouseEvent(Mouse.MouseEventFlags.MOUSEEVENTF_LEFTDOWN);
        }
    }
}