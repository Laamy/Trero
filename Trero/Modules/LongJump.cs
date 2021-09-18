using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;
using Trero.Modules.vModuleExtra;

namespace Trero.Modules
{
    internal class LongJump : Module
    {

        public LongJump() : base("LongJump", (char)0x07, "Player")
        {
            addBypass(new BypassBox(new string[] { "Default", "Hive" }));
        } // Not defined
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
                    if (bypasses[0].curIndex == 0)
                    {
                        newVel.x *= 1.0669f;
                        newVel.y *= 1.038f;
                        newVel.z *= 1.0669f;
                    }
                    if (bypasses[0].curIndex == 1)
                    {
                        if (newVel.x < 3f)
                            newVel.x *= 1.0669f;
                        if (newVel.y < 3f)
                            newVel.y *= 1.038f;
                        if (newVel.z < 3f)
                            newVel.z *= 1.0669f;
                    }
                }
            }

            Game.velocity = newVel;
        }
    }
}
