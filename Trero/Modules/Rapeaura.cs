#region

using Trero.ClientBase;
using Trero.ClientBase.KeyBase;

#endregion

namespace Trero.Modules
{
    internal class Rapeaura : Module
    {
        public Rapeaura() : base("Rapeaura", (char)0x07, "Combat", "Sexaura + Killaura")
        {
        } // Not defined

        public override void OnTick()
        {
            if (Game.isNull) return;

            var plr = Game.getClosestPlayer();
            if (Game.position.Distance(plr.position) < 6f)
                Game.SexActor(plr);

            if (Game.isLookingAtEntity && MCM.isGameFocused())
                Mouse.MouseEvent(Mouse.MouseEventFlags.MOUSEEVENTF_LEFTDOWN);
        }
    }
}