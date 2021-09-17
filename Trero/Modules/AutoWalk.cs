#region

using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;

#endregion

namespace Trero.Modules
{
    internal class AutoWalk : Module
    {
        public AutoWalk() : base("AutoWalk", (char)0x07, "Player")
        {
        } // Not defined

        public override void OnTick()
        {
            var newVel = Game.velocity;

            var cy = (Game.rotation.y + 89.9f) * ((float)Math.PI / 180F);

            newVel.z = (float)Math.Sin(cy) * (4 / 9f);
            newVel.x = (float)Math.Cos(cy) * (4 / 9f);

            Game.velocity = newVel;
        }
    }
}