#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class ReverseStep : Module
    {
        public ReverseStep() : base("ReverseStep", (char)0x07, "Movement", "Like step but downwards - Xello!") { }

        public override void OnTick()
        {
            if (Game.onGround && Game.velocity.y < 0 && !(Game.velocity.y < -1f))
            {
                Vector3 vel = Game.velocity;
                vel.y = -1f;
                Game.velocity = vel;
            }
        }
    }
}