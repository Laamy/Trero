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
        public Bhop() : base("Bhop", (char)0x07, "Exploits")
        {
            addBypass(new BypassBox(new string[] { "Default", "Hive" }));
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

            if (bypasses[0].curIndex == 0)
            {
                MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity"), (float)Math.Cos(calYaw) * 0.7f);
                if (Game.touchingObject == 1) // jump for bhop
                    MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 4, 0.3f);
                //Console.WriteLine(Game.onGround2);
                MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 8,
                    (float)Math.Sin(calYaw) * 0.7f);
            }
            else // Hive bypass
            {
                MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity"), (float)Math.Cos(calYaw) * 0.5f);
                if (Game.touchingObject == 1) // jump for bhop
                    MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 4, 0.25f);
                //Console.WriteLine(Game.onGround2);
                MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 8,
                    (float)Math.Sin(calYaw) * 0.5f);
            }
        }
    }
}