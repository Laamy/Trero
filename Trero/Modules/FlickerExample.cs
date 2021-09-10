using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trero.ClientBase;

namespace Trero.Modules
{
    class FlickerExample : Module
    {
        int flicker = 0;
        public FlickerExample() : base("FlickerExample", (char)0x07, "Other") { } // 0x07 = no keybind

        public override void onTick()
        {
            if (Game.isNull) return;
            flicker++;

            if (flicker == 300)
            {
                flicker = 0;
                Game.onGround = false;
            }
        }
    }
}
