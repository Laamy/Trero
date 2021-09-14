#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class AirJump : Module
    {
        public AirJump() : base("AirJump", (char)0x07, "World")
        {
        }

        public override void OnTick()
        {
            if (!Game.isNull) Game.onGround = true;
        }
    }
}