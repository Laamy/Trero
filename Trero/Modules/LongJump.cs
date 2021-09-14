using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;

namespace Trero.Modules
{
    internal class LongJump : Module
    {

        static float flicker = 0;
        public LongJump() : base("LongJump", (char)0x07, "Player") { } // Not defined
        public override void OnEnable()
        {
            MCM.writeFloat(Game.localPlayer + VersionClass.GetData("velocity") + 4, 0.5f);
            base.OnEnable();
        }
        public override void OnTick()
        {
            Vector3 newVel = Game.velocity;

            if (newVel.y > 0)
            {
                if (Game.onGround == false)
                {
                    newVel.x *= 1.0669f;
                    newVel.y *= 1.038f;
                    newVel.z *= 1.0669f;
                }
            }
            Game.velocity = newVel;
        }
    }
}
