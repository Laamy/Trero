#region

using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;

#endregion

namespace Trero.Modules
{
    internal class BulkFly : Module
    {
        private static readonly float speed = 12f;
        private static int _flicker;

        public BulkFly() : base("BulkFly", (char)0x07, "Flies")
        {
        } // 0x07 = no keybind

        public override void OnTick()
        {
            if (Game.isNull) return;
            _flicker++;

            var newVel = Base.Vec3();

            var cy = (Game.bodyRots.y + 89.9f) * ((float)Math.PI / 180F);
            newVel.x = (float)Math.Cos(cy) * (speed / 9f);

            newVel.y = -0.05f;
            switch (_flicker)
            {
                case 360 / 32:
                {
                    var newPos = Game.position;
                    newPos.y += 0.005f;
                    newPos.z += 0.003f;
                    newPos.x += 0.003f;

                    if (Keymap.GetAsyncKeyState((char)Keys.LShiftKey))
                        newPos.y -= 0.05f;
                    if (Keymap.GetAsyncKeyState((char)Keys.Space))
                        newPos.y += 0.08f;

                    Game.position = newPos;
                    break;
                }
                case 360 / 16:
                {
                    var newPos = Game.position;
                    newPos.y -= 0.003f;
                    newPos.z -= 0.003f;
                    newPos.x -= 0.003f;
                    Game.position = newPos;
                    break;
                }
            }

            if (_flicker == 360 / 32)
                _flicker = 0;

            newVel.z = (float)Math.Sin(cy) * (speed / 9f);

            Game.velocity = newVel;
        }
    }
}