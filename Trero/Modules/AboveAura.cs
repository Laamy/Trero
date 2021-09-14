#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class AboveAura : Module
    {
        private int _flicker;

        public AboveAura() : base("AboveAura", (char)0x07, "Exploits")
        {
        } // Not defined


        public override void OnTick()
        {
            if (Game.isNull) return;
            _flicker++;
            if (_flicker != 4) return;
            _flicker = 0;
            var ent = Game.getClosestPlayer();
            if (ent == null) return;

            var pos = ent.position;

            if (!(Game.position.Distance(pos) < 6)) return;
            pos.y += 2;

            Game.position = pos;
            Game.velocity = Base.Vec3(0, -0.01f);
        }
    }
}