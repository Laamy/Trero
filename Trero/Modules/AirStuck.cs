#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class AirStuck : Module
    {
        public AirStuck() : base("AirStuck", (char)0x07, "Player")
        {
        } // 0x07 = no keybind

        public override void OnTick()
        {
            if (!Game.isNull) Game.velocity = Base.Vec3();
        }
    }
}