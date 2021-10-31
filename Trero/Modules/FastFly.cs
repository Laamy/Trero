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
    internal class FastFly : Module
    {
        int a = 0;
        public FastFly() : base("FastFly", (char)0x07, "Flies", "Bypassing fly made for mineplex & lifeboat (Speed 4 recm)") // I'll add more settings to this later
        {
            addBypass(new BypassBox(new string[] { "UpTeleport: 0.5f", "UpTeleport: 1f", "UpTeleport: None" }));
            addBypass(new BypassBox(new string[] { "Speed: 2f", "Speed: 3f", "Speed: 4f", "Speed: 5f", "Speed: 1f" }));
            addBypass(new BypassBox(new string[] { "FreezeOnDisable: True", "FreezeOnDisable: False" }));
        } // Not defined

        public override void OnEnable()
        {
            base.OnEnable();

            var pos = Game.position;

            switch (bypasses[0].curIndex)
            {
                case 0:
                    pos.y += 0.5f;
                    break;
                case 1:
                    pos.y += 1f;
                    break;
                case 2:
                    break;
            }

            Game.teleport(pos);
        }

        public override void OnTick()
        {
            if (Game.isNull) return;

            a++;

            var speedMod = 2f;

            switch (bypasses[1].curIndex)
            {
                case 0:
                    speedMod = 2f;
                    break;
                case 1:
                    speedMod = 3f;
                    break;
                case 2:
                    speedMod = 4f;
                    break;
                case 3:
                    speedMod = 5f;
                    break;
                case 4:
                    speedMod = 1f;
                    break;
            }

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
                Game.teleport(pos);
            }
        }

        public override void OnDisable()
        {
            base.OnDisable();

            if (bypasses[2].curIndex == 0)
                Game.velocity = Base.Vec3();
        }
    }
}