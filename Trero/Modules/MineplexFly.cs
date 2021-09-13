using System;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.VersionBase;

namespace Trero.Modules
{
    class MineplexFly : Module
    {
        static float speed = 6.3f; // 5.6f best value
        static float flicker = 0;
        public MineplexFly() : base("MineplexFly", (char)0x07, "Flies") { } // Not defined
        public override void onEnable()
        {
            MCM.writeFloat(Game.localPlayer + VersionClass.getData("velocity") + 4, 0.5f);
            base.onEnable();
        }
        public override void onTick()
        {
            if (Game.isNull) return;

            Vector3 newVel = Base.Vec3();

            float cy = (Game.rotation.y + 89.9f) * ((float)Math.PI / 180F);

            newVel.x = (float)Math.Cos(cy) * (speed / 9f);
            newVel.y = -0.15f;
            newVel.z = (float)Math.Sin(cy) * (speed / 9f);

            Game.velocity = newVel;

            flicker++;

            if (flicker > 5)
            {
                flicker = 0;

                Vector3 pos = Game.position;

                pos.y += 0.36f;

                Game.position = pos;
            }
        }
    }
}
