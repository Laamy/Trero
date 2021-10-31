#region

using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;

#endregion

namespace Trero.Modules
{
    internal class OGMFly : Module
    {
        public OGMFly() : base("OGMFly", (char)0x07, "Flies", "Old version of mineplex fly")
        {
        } // Not defined

        public override void OnEnable()
        {
            base.OnEnable();

           
        }

        public override void OnTick() // hopefully some people remember this fly
        {
            if (Game.isNull) return;

            var speedMod = 0.7f;
            var calcYaw = (Game.bodyRots.y + 90f) * ((float)Math.PI / 180f);

            var newVel = Base.Vec3();

            newVel.x = (float)Math.Cos(calcYaw) * speedMod;
            newVel.y = 0.075f * speedMod;
            if (Keymap.GetAsyncKeyState(Keys.LShiftKey) || Keymap.GetAsyncKeyState(Keys.RShiftKey))
                newVel.y = -(0.05f * speedMod);
            newVel.z = (float)Math.Sin(calcYaw) * speedMod;

            Game.velocity = newVel;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            Game.velocity = Base.Vec3();
        }
    }
}