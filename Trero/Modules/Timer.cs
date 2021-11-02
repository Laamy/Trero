#region

using Trero.ClientBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class Timer : Module
    {
        public Timer() : base("Timer", (char)0x07, "World", "Change the games internal timer")
        {
            addBypass(new BypassBox(new string[] { "Speed: x2", "Speed: x3", "Speed: x5", "Speed: x0.5" }));
        }

        public override void OnEnable()
        {
            base.OnEnable();

            switch (bypasses[0].curIndex)
            {
                case 0:
                    Game.timer = 40;
                    break;
                case 1:
                    Game.timer = 60;
                    break;
                case 2:
                    Game.timer = 100;
                    break;
                case 3:
                    Game.timer = 10;
                    break;
            }
        }

        public override void OnDisable()
        {
            base.OnDisable();

            Game.timer = 20;
        }
    }
}