#region

using System;
using Trero.ClientBase;
using Trero.ClientBase.VersionBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class Disabler : Module
    {
        private static int _flicker;
        public Disabler() : base("Disabler", (char)0x07, "World")
        {
            addBypass(new BypassBox(new string[] { "Speed: 0.5f", "Speed: 0.3f", "Speed: 0.7f" }));
            addBypass(new BypassBox(new string[] { "DownFlip: 0.1f", "DownFlip: 0.05f", "DownFlip: 0.2f" }));
            addBypass(new BypassBox(new string[] { "UpFlip: 0.3f", "UpFlip: 0.2f", "UpFlip: 0.4f" }));
            addBypass(new BypassBox(new string[] { "Flicker: 5", "Flicker: 10", "Flicker: 15" }));
            addBypass(new BypassBox(new string[] { "VClip: 0.36f", "VClip: 0.46f", "VClip: 0.26f" }));
        }

        public override void OnEnable()
        {
            base.OnEnable();
        }

        public override void OnTick()
        {
            if (Game.isNull) return;

            float speed = 0.5f;
            switch (bypasses[0].curIndex)
            {
                case 1:
                    speed = 0.3f;
                    break;
                case 2:
                    speed = 0.7f;
                    break;
            }

            float downFlip = 0.1f;
            switch (bypasses[1].curIndex)
            {
                case 1:
                    downFlip = 0.05f;
                    break;
                case 2:
                    downFlip = 0.2f;
                    break;
            }

            float upFlip = 0.3f;
            switch (bypasses[2].curIndex)
            {
                case 1:
                    upFlip = 0.2f;
                    break;
                case 2:
                    upFlip = 0.4f;
                    break;
            }

            int Flicker = 5;
            switch (bypasses[3].curIndex)
            {
                case 1:
                    Flicker = 10;
                    break;
                case 2:
                    Flicker = 15;
                    break;
            }

            float vclip = 0.36f;
            switch (bypasses[4].curIndex)
            {
                case 1:
                    vclip = 0.26f;
                    break;
                case 2:
                    vclip = 0.46f;
                    break;
            }

            bool ongr = true;

            if (Game.velocity.x > speed || Game.velocity.x < -speed
                || Game.velocity.z > speed || Game.velocity.z < -speed)
            {
                ongr = false;
                Game.vflip(-downFlip);
            }

            if (Game.touchingObject == 1 && !ongr)
                Game.vflip(upFlip);

            _flicker++;

            if (!(_flicker > Flicker)) return;
            _flicker = 0;

            if (Game.onGround2 == false)
            {
                if (!ongr)
                {
                    Game.vclip(vclip);
                }
            }
        }
    }
}