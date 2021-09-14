#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class Rapeaura : Module
    {
        public Rapeaura() : base("Rapeaura", (char)0x07, "Combat")
        {
        } // Not defined

        public override void OnTick()
        {
            if (Game.isNull) return;

            var plr = Game.getClosestPlayer();
            if (!(Game.position.Distance(plr.position) < 6f)) return;
            Game.SexActor(plr);
            Game.Attack(plr);
        }
    }
}