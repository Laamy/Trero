#region

using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class HighJump : Module
    {
        public HighJump() : base("HighJump", (char)0x07, "Player")
        {
        } // 0x07 = no keybind

        public override void OnTick()
        {
            if (Game.isNull) return;
            //if (Keymap.GetAsyncKeyState(Keys.Space)) ; - Left empty so I commented out.
            Game.velocity = Base.Vec3(0, 5);
        }
    }
}