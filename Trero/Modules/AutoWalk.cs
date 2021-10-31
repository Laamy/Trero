#region

using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class AutoWalk : Module
    {
        public AutoWalk() : base("AutoWalk", (char)0x07, "Player", "Automatically walk forwards")
        {
            addBypass(new BypassBox(new string[] { "Normal", "Fast", "Slow" }));
        } // Not defined

        public override void OnTick()
        {
            var newVel = Game.velocity;

            var cy = (Game.bodyRots.y + 89.9f) * ((float)Math.PI / 180F);

            int speed = 4;

            if (bypasses[0].curIndex == 1)
                speed = 6;
            if (bypasses[0].curIndex == 2)
                speed = 2;

            newVel.z = (float)Math.Sin(cy) * (speed / 9f);
            newVel.x = (float)Math.Cos(cy) * (speed / 9f);

            Game.velocity = newVel;
        }
    }
}