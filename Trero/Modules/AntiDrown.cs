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
    internal class AntiDrown : Module
    {
        public AntiDrown() : base("AntiDrown", (char)0x07, "Player")
        {
            addBypass(new BypassBox(new string[] { "Speed: Slow", "Speed: Normal", "Speed: Fast" }));
        }

        public override void OnTick()
        {
            if (Game.isNull) return;

            if (Game.isInWater)
            {
                float speed = 0.1f;
                switch (bypasses[0].curIndex)
                {
                    case 0:
                        speed = 0.1f;
                        break;
                    case 1:
                        speed = 0.3f;
                        break;
                    case 2:
                        speed = 0.6f;
                        break;
                }
                Game.velocity = Base.Vec3(0, speed);
            }
        }
    }
}
