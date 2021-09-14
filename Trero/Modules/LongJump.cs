using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;

namespace Trero.Modules
{
    class LongJump : Module
    {
        
        static float flicker = 0;
        public LongJump() : base("LongJump", (char)0x07, "Player") { } // Not defined
        public override void onEnable()
        {
            MCM.writeFloat(Game.localPlayer + VersionClass.getData("velocity") + 4, 0.5f);
            base.onEnable();
        }
        public override void onTick()
        {
            Vector3 newVel = Game.velocity;

            if (newVel.y > 0)
            {
                if (Game.onGround == false)
                {
                    newVel.x *= 1.051f;
                    newVel.y *= 1.063f;
                    newVel.z *= 1.051f;
                }
            }
            Game.velocity = newVel;
        }
    }
}
