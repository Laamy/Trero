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
    internal class HiveFly : Module
    {
        public HiveFly() : base("HiveFly", (char)0x07, "Flies")
        {
            addBypass(new BypassBox(new string[] { "Default", "Fast", "Super Fast", "Super Slow", "Slow" }));
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

            var speedMod = 0.7f; // 0.7f

            switch (bypasses[0].curIndex)
            {
                case 0:
                    speedMod = 0.7f;
                    break;
                case 1:
                    speedMod = 0.9f;
                    break;
                case 2:
                    speedMod = 1.2f;
                    break;
                case 3:
                    speedMod = 0.3f;
                    break;
                case 4:
                    speedMod = 1.5f;
                    break;
            }

            var calcYaw = (Game.bodyRots.y + 90f) * ((float)Math.PI / 180f);

            var newVel = Base.Vec3();

            newVel.x = (float)Math.Cos(calcYaw) * speedMod;
            newVel.y = 0.05f * speedMod;
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