#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class Phase : Module
    {
        public Phase() : base("Phase", (char)0x07, "Exploits")
        {
        } // Not defined

        public override void OnTick()
        {
            var pos = Game.position;
            var pos2 = Game.position;

            pos2.x += 0.6f;
            pos2.z += 0.6f;

            Game.teleport(new AABB(pos, pos2));
        }

        public override void OnDisable()
        {
            base.OnDisable();
            Game.teleport(Game.position);
        }
    }
}