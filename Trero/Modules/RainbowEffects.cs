#region

using System;
using Trero.ClientBase;

#endregion

namespace Trero.Modules
{
    internal class RainbowEffects : Module
    {
        public RainbowEffects() : base("RainbowEffects", (char)0x07, "Visual", false)
        {
        } // Not defined

        public override void OnEnable()
        {
            base.OnEnable();
        }

        public Random ran = new Random();

        public override void OnTick()
        {
            iRGB c = Game.effectsColor;

            /*c.R += 1;
            c.G += 2;
            c.B += 3;

            if (c.R > 254) c.R = 0;
            if (c.G > 254) c.G = 0;
            if (c.B > 254) c.B = 0;*/

            c.R = (byte)ran.Next(0, 255);
            c.G = (byte)ran.Next(0, 255);
            c.B = (byte)ran.Next(0, 255);

            Game.effectsColor = c;

            base.OnTick();
        }

        public override void OnDisable()
        {
            Game.effectsColor = new iRGB(0, 0, 0, 0);

            base.OnDisable();
        }
    }
}