using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.EntityBase;
using Trero.ClientBase.KeyBase;

namespace Trero.Modules
{
    class TriggerBot : Module
    {
        public TriggerBot() : base("TriggerBot", (char)0x07, "Combat") { } // Not defined

        public override void onTick()
        {
            if (Game.isNull) return;

            if (MCM.isMinecraftFocused() && Game.isLookingAtEntity) // TODO: antibot
            {
                if (Game.position.Distance(Game.getClosestPlayer().position) < 7f)
                    new Thread(() => Mouse.MouseEvent(Mouse.MouseEventFlags.MOUSEEVENTF_LEFTDOWN)).Start();
            }
        }
    }
}
