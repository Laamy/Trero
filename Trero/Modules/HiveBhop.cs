#region

using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class HiveBhop : Module
    {
        public HiveBhop() : base("HiveBhop", (char)0x07, "Player")
        {
        addBypass(new BypassBox(new string[] { "Speed: Slow", "Speed: Normal", "Speed: Fast" }));
        }

        int jumpDelay = 0;

        public override void OnTick()
        {
            if (Game.isNull) return;

            if (Game.onGround2 == false)
            {
                var newVel = Game.velocity;

                var cy = (Game.bodyRots.y + 89.9f) * ((float)Math.PI / 180F);

                if (Keymap.GetAsyncKeyState(Keys.W) || Keymap.GetAsyncKeyState(Keys.A) || Keymap.GetAsyncKeyState(Keys.S) || Keymap.GetAsyncKeyState(Keys.D))
                {
                    if (Keymap.GetAsyncKeyState(Keys.A))
                        cy += 55;
                    if (Keymap.GetAsyncKeyState(Keys.S))
                        cy += 110;
                    if (Keymap.GetAsyncKeyState(Keys.D))
                        cy -= 55;

                    int speed = 3;

                    switch (bypasses[0].curIndex)
                    {
                        case 0:
                            speed = 3;
                            break;
                        case 1:
                            speed = 5;
                            break;
                        case 2:
                            speed = 7;
                            break;
                    }

                    newVel.z = (float)Math.Sin(cy) * (speed / 9f);
                    newVel.x = (float)Math.Cos(cy) * (speed / 9f);

                    Game.velocity = newVel;

                    if (jumpDelay != 0)
                        jumpDelay = 0;
                }
            }
            else
            {
                if (Keymap.GetAsyncKeyState(Keys.W) || Keymap.GetAsyncKeyState(Keys.A) || Keymap.GetAsyncKeyState(Keys.S) || Keymap.GetAsyncKeyState(Keys.D))
                {
                    jumpDelay++;
                    if (jumpDelay == 2)
                        MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 4, 0.31f);
                }
            }
        }
    }
}
