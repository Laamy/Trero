#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class ReverseStep : Module
    {
        public ReverseStep() : base("ReverseStep", (char)0x07, "Movement") { }

        public override void OnTick()
        {
            if (onGround && velocity.y < 0 && !(velocity.y < -1f))
            {
                Vector3 vel = velocity;
                vel.y = -1f;
                velocity = vel;
            }
        }
    }
}