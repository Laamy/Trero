#region

using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;

#endregion

namespace Trero.Modules
{
    internal class Fly : Module
    {
        public Fly() : base("Fly", (char)0x07, "Flies")
        {
        } // 0x07 = no keybind

        public override void OnEnable()
        {
            base.OnEnable();

            foreach (var mod in Program.Modules)
                if (mod.name == "Disabler" && mod.enabled)
                    Game.vclip(0.5f);
        }

        public override void OnTick()
        {
            if (Game.isNull) return;

            Game.onGround = true;

            var newVel = Base.Vec3();

            var cy = (Game.bodyRots.y + 89.9f) * ((float)Math.PI / 180F);

            if (Keymap.GetAsyncKeyState(Keys.W))
            {
                newVel.z = (float)Math.Sin(cy) * (12 / 16f);
                newVel.x = (float)Math.Cos(cy) * (12 / 16f);
            }

            if (Keymap.GetAsyncKeyState((char)Keys.Space))
            {
                foreach (var mod in Program.Modules)
                    if (mod.name == "Disabler" && !mod.enabled)
                        newVel.y += 0.6f;
            }
            if (Keymap.GetAsyncKeyState((char)Keys.LShiftKey))
            {
                newVel.y -= 0.6f;
            }

            Game.velocity = newVel;
        }
    }
}