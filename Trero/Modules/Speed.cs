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
    internal class Speed : Module
    {
        public Speed() : base("Speed", (char)0x07, "Player", "Go super fast!")
        {
            addBypass(new BypassBox(new string[] { "Client: Trero", "Client: Vanilla" }));
            addBypass(new BypassBox(new string[] { "Speed: 1", "Speed: 2", "Speed: 3" }));
        } // Not defined

        public override void OnDisable()
        {
            base.OnDisable();

            Game.speed = 0.1000000015f;
        }

        public override void OnTick()
        {
            if (/* Game.inInventory || */ Game.isNull) return;

            switch (bypasses[0].curIndex)
            {
                case 0:
                    var plrYaw = Game.bodyRots.y; // yaw
                    float _speed = 0.7f;

                    switch (bypasses[1].curIndex)
                    {
                        case 0:
                            _speed = 0.7f;
                            break;
                        case 1:
                            _speed = 1f;
                            break;
                        case 2:
                            _speed = 1.5f;
                            break;
                    }

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
                    MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 8,
                        (float)Math.Sin(calYaw) * (_speed / 5));
                    break;
                case 1:
                    Game.speed = (((bypasses[1].curIndex + 1) * 0.0200000009f) + 0.1f) * 2;
                    break;
            }
        }
    }
}