#region

using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class Jetpack : Module
    {
        public Jetpack() : base("Jetpack", (char)0x07, "Flies", "Fly forwards a {x} speed")
        {
            addBypass(new BypassBox(new string[] { "Default", "Fast", "Hyper speed", "Slow" }));
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

            float speed = 0.8f;

            if (bypasses[0].curIndex == 0)
                speed = 0.8f;
            if (bypasses[0].curIndex == 1)
                speed = 1.2f;
            if (bypasses[0].curIndex == 2)
                speed = 12f;
            if (bypasses[0].curIndex == 3)
                speed = 0.4f;

            vel.x = speed * dirVec.x;
            vel.y = speed * -dirVec.y;
            vel.z = speed * dirVec.z;

            Game.velocity = vel;
        }
    }
}