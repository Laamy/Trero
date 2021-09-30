#region

using Trero.ClientBase;
using Trero.Modules.vModuleExtra;

#endregion

namespace Trero.Modules
{
    internal class RapidPlace : Module
    {
        public RapidPlace() : base("RapidPlace", (char)0x07, "Player")
        {
        }

        public override void OnTick()
        {
            if (Game.isNull) return;

            Game.Placing = 0;
        }
    }
}