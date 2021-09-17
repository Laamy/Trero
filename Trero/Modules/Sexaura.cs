#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class Sexaura : Module
    {
        public Sexaura() : base("Sexaura", (char)0x07, "World")
        {
        } // Not defined

        public override void OnTick()
        {
            if (Game.isNull) return;

            var plr = Game.getClosestPlayer();
            if (Game.position.Distance(plr.position) < 6f)
                Game.SexActor(plr);
        }
    }
}