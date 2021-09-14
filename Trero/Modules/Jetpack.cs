#region

using Trero.ClientBase;
using Trero.ClientBase.KeyBase;

#endregion

namespace Trero.Modules
{
    internal class Jetpack : Module
    {
        public Jetpack() : base("Jetpack", (char)0x07, "Flies")
        {
            Keymap.keyEvent += KeyvE;
        } // Not defined

        private void KeyvE(object sender, KeyEvent e)
        {
            if (e.vkey != VKeyCodes.KeyUp) return;
            if ((char)(int)e.key == keybind) OnDisable(); // Disable module when keybind has been let go
        }

        public override void OnTick()
        {
            if (Game.isNull) return;

            var vel = Base.Vec3();
            var dirVec = Game.lVector;

            vel.x = 0.6f * dirVec.x;
            vel.y = 0.6f * -dirVec.y;
            vel.z = 0.6f * dirVec.z;

            Game.velocity = vel;
        }
    }
}