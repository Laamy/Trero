#region

using Trero.ClientBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class Sexaura : Module
    {
        public Sexaura() : base("Sexaura", (char)0x07, "World", "Tp around the closest players head")
        {
            addBypass(new BypassBox(new string[] { "Mobaura: False", "Mobaura: True" }));
            addBypass(new BypassBox(new string[] { "Plus 0.3", "Plus 0.5" }));
            addBypass(new BypassBox(new string[] { "Plus 1", "Plus 1.5" }));
        } // Not defined

        public override void OnTick()
        {
            if (Game.isNull) return;

            float x = 0.3f;
            float y = 1f;

            if (bypasses[0].curIndex == 0)
                x = 0.3f;
            if (bypasses[0].curIndex == 1)
                x = 0.5f;

            if (bypasses[1].curIndex == 0)
                y = 1f;
            if (bypasses[1].curIndex == 1)
                y = 1.5f;

            var plr = Game.getClosestPlayer();
            if (Game.position.Distance(plr.position) < 6f)
                Game.SexActor(plr, x, y);
        }
    }
}