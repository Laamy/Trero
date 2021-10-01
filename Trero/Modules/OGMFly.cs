#region

using System;
using Trero.ClientBase;
using Trero.ClientBase.VersionBase;

#endregion

namespace Trero.Modules
{
    internal class OGMFly : Module
    {
        public OGMFly() : base("OGMFly", (char)0x07, "Flies")
        {
        } // Not defined

        public override void OnEnable()
        {
            base.OnEnable();

            var pos = Game.position;
            pos.y += 0.5f;
            Game.position = pos;
        }

        public override void OnTick() // hopefully some people remember this fly
        {
            if (Game.isNull) return;

            var speedMod = 0.7f;
            var calcYaw = (Game.bodyRots.y + 90f) * ((float)Math.PI / 180f);

            var newVel = Base.Vec3();

            newVel.x = (float)Math.Cos(calcYaw) * speedMod;
            newVel.y = -0.001f * speedMod;
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