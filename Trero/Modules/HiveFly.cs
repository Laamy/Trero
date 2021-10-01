#region

using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;

#endregion

namespace Trero.Modules
{
    internal class HiveFly : Module
    {
        public HiveFly() : base("HiveFly", (char)0x07, "Flies")
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
            var speed = 0.4f;

            var calcYaw = (Game.bodyRots.y + 90f) * ((float)Math.PI / 180f);

            var newVel = Base.Vec3();

            newVel.x = (float)Math.Cos(calcYaw) * speed;
            newVel.y = -0.10f * speed;
            newVel.z = (float)Math.Sin(calcYaw) * speed;

            Game.velocity = newVel;

            var pos = Game.position;
            pos.y += speed / 64;
            Game.teleport(new AABB(pos, new Vector3(pos.x + .6f, pos.y - 1.8f, pos.z + .6f))); // noclip flight
        }
        public override void OnDisable()
        {
            base.OnDisable();

            Game.teleport(Game.position); // reset my player hitbox

            var speed = 0.3f;

            var calcYaw = (Game.bodyRots.y + 90f) * ((float)Math.PI / 180f);

            var newVel = Base.Vec3();

            newVel.x = (float)Math.Cos(calcYaw) * speed;
            newVel.y = -0.01f;
            newVel.z = (float)Math.Sin(calcYaw) * speed;

            Game.velocity = newVel;
        }
    }
}