using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.UIBase;

namespace Trero.Modules
{
    class NoYFly : Module
    {
        public NoYFly() : base("NoYFly", (char)0x07, "Flies") { } // Not defined
        public override void onTick()
        {
            Game.onGround = true;

            Game.velocity = Base.Vec3();

            Vector3 newVel = Base.Vec3();

            float cy = (Game.rotation.y + 89.9f) * ((float)Math.PI / 180F);

            if (Keymap.GetAsyncKeyState((char)(Keys.W)))
                newVel.z = (float)Math.Sin(cy) * (8 / 9f); ///Working Fly With No Height 


            if (Keymap.GetAsyncKeyState((char)(Keys.W)))
                newVel.x = (float)Math.Cos(cy) * (8 / 9f);
            Game.velocity = newVel;
        }
    }
}
