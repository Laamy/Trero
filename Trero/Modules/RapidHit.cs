#region

using Trero.ClientBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class RapidHit : Module
    {
        public RapidHit() : base("RapidHit", (char)0x07, "Combat")
        {
        }

        public override void OnTick()
        {
            if (Game.isNull) return;

            Game.Hitting = 0;
        }
    }
}