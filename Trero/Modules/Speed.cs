#region

using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;

#endregion

namespace Trero.Modules
{
    internal class Speed : Module
    {
        private readonly float _speed = 0.7f;

        public Speed() : base("Speed", (char)0x07, "Player")
        {
        } // Not defined

        public override void OnTick()
        {
            if (/* Game.inInventory || */ Game.isNull) return;

            var plrYaw = Game.bodyRots.y; // yaw

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
                (float)Math.Sin(calYaw) * _speed);
        }
    }
}