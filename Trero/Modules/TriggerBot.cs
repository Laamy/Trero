#region

using System.Threading;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;

#endregion

namespace Trero.Modules
{
    internal class TriggerBot : Module
    {
        public TriggerBot() : base("TriggerBot", (char)0x07, "Combat")
        {
        } // Not defined

        public override void OnTick()
        {
            if (Game.isNull) return;

            if (!MCM.isMinecraftFocused() || !Game.isLookingAtEntity) return;
            if (Game.position.Distance(Game.getClosestPlayer().position) < 7f)
                new Thread(() => Mouse.MouseEvent(Mouse.MouseEventFlags.MOUSEEVENTF_LEFTDOWN)).Start();
        }
    }
}