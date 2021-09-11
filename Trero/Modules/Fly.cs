using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;

namespace Trero.Modules
{
    class Fly : Module
    {
        public Fly() : base("Fly", (char)0x07, "Flies") { } // 0x07 = no keybind

        public override void onTick()
        {
            if (Game.isNull) return;

            Game.onGround = true;

            Vector3 newVel = Base.Vec3();

            float cy = (Game.rotation.y + 89.9f) * ((float)Math.PI / 180F);

            if (Keymap.GetAsyncKeyState(Keys.W))
            {
                newVel.z = (float)Math.Sin(cy) * (12 / 16f);
                newVel.x = (float)Math.Cos(cy) * (12 / 16f);
            }

            if (Keymap.GetAsyncKeyState((char)(Keys.Space)))
                newVel.y += 0.6f;
            if (Keymap.GetAsyncKeyState((char)(Keys.LShiftKey))) ///Working Fly 
                newVel.y -= 0.6f;

            Game.velocity = newVel;
        }
    }
}
