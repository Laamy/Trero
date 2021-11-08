#region

using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class Fly : Module
    {
        private int count = 0;//lifeboatfly blink counter
        private bool isBlinking = false;

        public Fly() : base("LifeboatFly", (char)0x07, "Flies", "Basic fly that supports minevile's disabler - Gamerclient28921 (Improved by yaami<3#3731)")
        {
        } // 0x07 = no keybind

        public override void OnEnable()
        {
            base.OnEnable();

            isBlinking = false;
            Game.position = new Vector3(Game.position.x, Game.position.y + 1, Game.position.z);
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

            if (count > 7)
            {
                isBlinking = true;
                OverrideBase.CanSendPackets = false;
                count = 0;
            }
            else
            {
                count++;
                isBlinking = false;
                OverrideBase.CanSendPackets = true;
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
                OverrideBase.CanSendPackets = true;
                isBlinking = false;
            }
            Game.timer = 20f;
            Game.velocity = Base.Vec3();
        }
    }
}