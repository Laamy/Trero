#region

using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;

#endregion

namespace Trero.Modules
{
    internal class FastFly : Module
    {
        int a = 0;
        public FastFly() : base("FastFly", (char)0x07, "Flies")
        {
        } // Not defined

        public override void OnEnable()
        {
            base.OnEnable();

            var pos = Game.position;
            pos.y += 0.5f;
            Game.position = pos;
        }

        public override void OnTick()
        {
            if (Game.isNull) return;

            a++;

            var speedMod = 2f;
            var calcYaw = (Game.bodyRots.y + 90f) * ((float)Math.PI / 180f);

            var newVel = Base.Vec3();

            newVel.x = (float)Math.Cos(calcYaw) * speedMod;
            newVel.y = 0.05f * speedMod;
            newVel.z = (float)Math.Sin(calcYaw) * speedMod;

            Game.velocity = newVel;

            if (a == 30)
            {
                a = 0;
                var pos = Game.position;
                pos.y -= 1.25f;
                Game.position = pos;
            }
        }

        public override void OnDisable()
        {
            base.OnDisable();

            Game.velocity = Base.Vec3();
        }
    }
}