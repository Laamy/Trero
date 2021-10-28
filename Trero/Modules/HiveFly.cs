#region

using System;
using System.Threading.Tasks;
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
            addBypass(new BypassBox(new string[] { "Default", "Hive", "Nethergames", "Mineplex" }));
        } // Not defined

        public override void OnEnable()
        {
            base.OnEnable();

            var tempPos = Game.position;
            tempPos.y += 0.25f;
            Game.teleport(tempPos);
        }

        public override void OnTick()
        {
            if (Game.isNull) return;

            var speedMod = 0.7f; // 0.7f

            switch (bypasses[0].curIndex)
            {
                case 0:
                    speedMod = 0.7f;
                    break;
                case 1:
                    speedMod = 0.2f;
                    break;
                case 2:
                    speedMod = 2f;
                    break;
                case 3:
                    speedMod = 1.6f;
                    break;
            }

            var calcYaw = (Game.bodyRots.y + 90f) * ((float)Math.PI / 180f);

            var newVel = Base.Vec3();

            newVel.x = (float)Math.Cos(calcYaw) * speedMod;
            //newVel.y = -0.05f * speedMod;
            newVel.z = (float)Math.Sin(calcYaw) * speedMod;

            Game.velocity = newVel;

            var tempPos = Game.position;
            tempPos.y -= 0.005f;
            Game.teleport(tempPos);
        }
    }
}