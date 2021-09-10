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
    class BulkFly : Module
    {
        static float speed = 12f; // 5.6f
        static int flicker = 0;
        public BulkFly() : base("BulkFly", (char)0x07) { } // 0x07 = no keybind

        public override void onTick()
        {
            if (Game.isNull) return;
            flicker++;

            Vector3 newVel = Base.Vec3();

            float cy = (Game.rotation.y + 89.9f) * ((float)Math.PI / 180F);
            newVel.x = (float)Math.Cos(cy) * (speed / 9f);

            newVel.y = -0.05f;
            if (flicker == 360 / 32)
            {
                Vector3 newPos = Game.position;
                newPos.y += 0.005f;
                newPos.z += 0.003f;
                newPos.x += 0.003f;

                if (Keymap.GetAsyncKeyState((char)(Keys.LShiftKey)))
                    newPos.y -= 0.05f;
                if (Keymap.GetAsyncKeyState((char)(Keys.Space)))
                    newPos.y += 0.08f;

                Game.position = newPos;
            }
            if (flicker == (360 / 16))
            {
                Vector3 newPos = Game.position;
                newPos.y -= 0.003f;
                newPos.z -= 0.003f;
                newPos.x -= 0.003f;
                Game.position = newPos;
            }

            if (flicker == 360 / 32)
                flicker = 0;

            newVel.z = (float)Math.Sin(cy) * (speed / 9f);

            Game.velocity = newVel;
        }
    }
}
