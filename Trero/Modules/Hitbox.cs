#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class Hitbox : Module
    {
        public Hitbox() : base("Hitbox", (char)0x07, "Combat")
        {
        } // 0x07 = no keybind

        public override void OnTick()
        {
            if (Game.isNull) return;
            foreach (var entity in Game.getPlayers()) entity.hitbox = Base.Vec2(7, 7);
        }

        public override void OnDisable()
        {
            base.OnDisable();

            foreach (var entity in Game.getPlayers()) entity.hitbox = Base.Vec2(0.6f, 1.8f);
        }
    }
}