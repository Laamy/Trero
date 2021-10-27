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
            addBypass(new BypassBox(new string[] { "Speed: 1", "Speed: 2", "Speed: 3" }));
        }

        public override void OnTick()
        {
            if (Game.isNull) return;

            if (Game.onGround2)
            {
                var plrYaw = Game.bodyRots.y; // yaw
                float _speed = 0.7f;

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
                {
                    if (!Keymap.GetAsyncKeyState(Keys.A) && Keymap.GetAsyncKeyState(Keys.D))
                        plrYaw += 180f;
                }

                if (!(Keymap.GetAsyncKeyState(Keys.W) | Keymap.GetAsyncKeyState(Keys.A) | Keymap.GetAsyncKeyState(Keys.S) |
                      Keymap.GetAsyncKeyState(Keys.D))) return;
                var calYaw = plrYaw * ((float)Math.PI / 180f);

                MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity"), (float)Math.Cos(calYaw) * _speed);
                MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 8, (float)Math.Sin(calYaw) * (_speed / 5));
            }
            else
            {
                MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 4, 0.4f);
            }
        }
    }
}