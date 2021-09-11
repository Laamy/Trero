using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;

namespace Trero.Modules
{
    class Step : Module
    {
        public Step() : base("Step", (char)0x07, "Step") { } // 0x07 = no keybind

        public override void onTick()
        {
            if (Game.isNull) return;

            Game.stepHeight = 2f;
        }
    }
}
