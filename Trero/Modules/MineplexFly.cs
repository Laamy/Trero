#region

using System;
using Trero.ClientBase;
using Trero.ClientBase.VersionBase;

#endregion

namespace Trero.Modules
{
    internal class MineplexFly : Module
    {
        private const float Speed = 6.3f; // 5.6f best value
        private static float _flicker;

        public MineplexFly() : base("MineplexFly", (char)0x07, "Flies", "Fly designed for mineplex")
        {
        } // Not defined

        public override void OnEnable()
        {
            Game.vclip(0.5f);

            base.OnEnable();
        }

        public override void OnTick()
        {
            if (Game.isNull) return;

            var newVel = Base.Vec3();

            var cy = (Game.bodyRots.y + 89.9f) * ((float)Math.PI / 180F);

            newVel.x = (float)Math.Cos(cy) * (Speed / 9f);
            newVel.y = -0.10f;
            newVel.z = (float)Math.Sin(cy) * (Speed / 9f);

            Game.velocity = newVel;

            _flicker++;

            if (!(_flicker > 5)) return;
            _flicker = 0;

            var pos = Game.position;

            pos.y += 0.36f;

            Game.position = pos;

            if (Game.touchingObject == 1)
                Game.vflip(0.3f);
        }
    }
}