#region

using System;
using Trero.ClientBase;
using Trero.ClientBase.VersionBase;

#endregion

namespace Trero.Modules
{
    internal class Disabler : Module
    {
        private static int _flicker;
        public Disabler() : base("Disabler", (char)0x07, "World") { }

        public override void OnEnable()
        {
            base.OnEnable();
        }

        public override void OnTick()
        {
            if (Game.isNull) return;

            bool ongr = true;

            if (Game.velocity.x > 0.5f || Game.velocity.x < -0.5f
                || Game.velocity.z > 0.5f || Game.velocity.z < -0.5f)
            {
                ongr = false;
                Game.vflip(-0.10f);
            }

            if (Game.touchingObject == 1 && !ongr)
                Game.vflip(0.3f);

            _flicker++;

            if (!(_flicker > 5)) return;
            _flicker = 0;

            if (Game.onGround2 == false)
            {
                if (!ongr)
                {
                    Game.vclip(0.36f);
                }
            }
        }
    }
}