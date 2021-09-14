#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class PhaseUp : Module
    {
        public PhaseUp() : base("PhaseUp", (char)0x07, "Flies")
        {
        } // Not defined

        public override void OnTick()
        {
            Game.velocity = Base.Vec3();

            var newPos = Game.position;
            newPos.y += 0.01f;
            Game.position = newPos;
        }
    }
}