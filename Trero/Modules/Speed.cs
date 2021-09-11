using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;

namespace Trero.Modules
{
    class Speed : Module
    {
        float speed = 0.7f;
        public Speed() : base("Speed", (char)0x07, "Player") { } // Not defined
        public override void onTick()
        {
            var plrYaw = Game.rotation.y; // yaw

            if (Keymap.GetAsyncKeyState(Keys.W))
            {
                if (!Keymap.GetAsyncKeyState(Keys.A) && !Keymap.GetAsyncKeyState(Keys.D))
                    plrYaw += 90f;
                if (Keymap.GetAsyncKeyState(Keys.A))
                    plrYaw += 45f;
                else if (Keymap.GetAsyncKeyState(Keys.D))
                    plrYaw += 135f;
            }
            else if (Keymap.GetAsyncKeyState(Keys.S))
            {
                if (!Keymap.GetAsyncKeyState(Keys.A) && !Keymap.GetAsyncKeyState(Keys.D))
                    plrYaw -= 90f;
                if (Keymap.GetAsyncKeyState(Keys.A))
                    plrYaw -= 45f;
                else if (Keymap.GetAsyncKeyState(Keys.D))
                    plrYaw -= 135f;
            }
            else if (!Keymap.GetAsyncKeyState(Keys.W) && !Keymap.GetAsyncKeyState(Keys.S))
                if (!Keymap.GetAsyncKeyState(Keys.A) && Keymap.GetAsyncKeyState(Keys.D))
                    plrYaw += 180f;

            if (Keymap.GetAsyncKeyState(Keys.W) | Keymap.GetAsyncKeyState(Keys.A) | Keymap.GetAsyncKeyState(Keys.S) | Keymap.GetAsyncKeyState(Keys.D))
            {

                float calYaw = (plrYaw) * ((float)Math.PI / 180f);

                MCM.writeFloat(Game.localPlayer + VersionClass.getData("velocity"), (float)Math.Cos(calYaw) * speed);
                /*if (Game.onGround2 == 257 && Keymap.GetAsyncKeyState(Keys.Space)) // jump for bhop
                    MCM.writeFloat(Game.localPlayer + VersionClass.getData("velocity") + 4, 0.3f);*/
                MCM.writeFloat(Game.localPlayer + VersionClass.getData("velocity") + 8, (float)Math.Sin(calYaw) * speed);
            }
        }
    }
}
