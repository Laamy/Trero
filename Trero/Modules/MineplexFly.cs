#region

using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;

#endregion

namespace Trero.Modules
{
    internal class MineplexFly : Module
    {
        private int count = 0;//lifeboatfly blink counter
        private bool isBlinking = false;

        public MineplexFly() : base("MineplexFly", (char)0x07, "Flies", "Fly designed for mineplex")
        {
        } // Not defined

        public override void OnEnable()
        {
            base.OnEnable();

            isBlinking = false;
            Game.position = new Vector3(Game.position.x, Game.position.y + 0.50f, Game.position.z);
        }

        public override void OnTick()
        {
            if (Game.isNull) return;

            float speed = 0.4f;

            Game.timer = 20f * speed;

            Game.onGround = true;

            var newVel = Base.Vec3();

            var cy = (Game.bodyRots.y + 90) * ((float)Math.PI / 180F);
            float speed2 = 1.25f / speed;

            if (Keymap.GetAsyncKeyState(Keys.W))
                MoveCharacter(cy, speed2, out newVel);
            if (Keymap.GetAsyncKeyState(Keys.A))
                MoveCharacter(cy + 55, speed2, out newVel);
            if (Keymap.GetAsyncKeyState(Keys.S))
                MoveCharacter(cy - 110, speed2, out newVel);
            if (Keymap.GetAsyncKeyState(Keys.D))
                MoveCharacter(cy - 55, speed2, out newVel);
            if (Keymap.GetAsyncKeyState(Keys.Space))
                Game.vclip(0.25f);
            if (Keymap.GetAsyncKeyState(Keys.LShiftKey) || Keymap.GetAsyncKeyState(Keys.RShiftKey))
                Game.vclip(-0.25f);

            if (count > 7)
            {
                isBlinking = true;
                //OverrideBase.CanSendPackets = false;
                count = 0;
            }
            else
            {
                count++;
                isBlinking = false;
                //OverrideBase.CanSendPackets = true;
            }
            Game.velocity = newVel;
        }

        public void MoveCharacter(float cy, float speed, out Vector3 vec)
        {
            Vector3 newVel = Base.Vec3(0, -0.001f);
            newVel.z = (float)Math.Sin(cy) * speed;
            newVel.x = (float)Math.Cos(cy) * speed;
            vec = newVel;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            count = 0;
            if (isBlinking)
            {
                //OverrideBase.CanSendPackets = true;
                isBlinking = false;
            }
            Game.timer = 20f;
            Game.velocity = Base.Vec3();
        }
    }
}