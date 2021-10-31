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
    internal class Bhop : Module
    {
        public Bhop() : base("Bhop", (char)0x07, "Exploits", "Hop around like a bunny :3")
        {
            addBypass(new BypassBox(new string[] { "Speed: 0.7f", "Speed: 0.5f", "Speed: 0.3f", "Speed: 1.5f", "Speed: 1f" }));
            addBypass(new BypassBox(new string[] { "Height: 0.3f", "Height: 0.2f", "Height: 0.1f", "Height: 0.05f" }));
        } // Not defined

        public override void OnTick()
        {
            if (/* Game.inInventory || */ Game.isNull) return;

            var plrYaw = Game.bodyRots.y; // yaw

            if (Keymap.GetAsyncKeyState(Keys.W))
            {
                if (!Keymap.GetAsyncKeyState(Keys.A) && !Keymap.GetAsyncKeyState(Keys.D))
                    plrYaw += 90f;
                else if (Keymap.GetAsyncKeyState(Keys.A))
                    plrYaw += 45f;
                else if (Keymap.GetAsyncKeyState(Keys.D))
                    plrYaw += 135f;
            }
            else if (Keymap.GetAsyncKeyState(Keys.S))
            {
                if (!Keymap.GetAsyncKeyState(Keys.A) && !Keymap.GetAsyncKeyState(Keys.D))
                    plrYaw -= 90f;
                else if (Keymap.GetAsyncKeyState(Keys.A))
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

            float speed = 0.7f;

            switch (bypasses[0].curIndex)
            {
                case 1:
                    speed = 0.5f;
                    break;
                case 2:
                    speed = 0.3f;
                    break;
                case 3:
                    speed = 1.5f;
                    break;
                case 4:
                    speed = 1f;
                    break;
            }

            float height = 0.3f;

            switch (bypasses[1].curIndex)
            {
                case 1:
                    height = 0.2f;
                    break;
                case 2:
                    height = 0.1f;
                    break;
                case 3:
                    height = 0.05f;
                    break;
            }

            MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity"), (float)Math.Cos(calYaw) * speed);

            if (Game.onGround2) // jump for bhop
            {
                MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 4, height);
            }

            MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 8, (float)Math.Sin(calYaw) * speed);
        }
    }
}