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
        public Step() : base("Step", (char)0x07, "Player") { } // 0x07 = no keybind

        public override void onEnable()
        {
            base.onEnable();
            Game.stepHeight = 2f;
        }

        public override void onDisable()
        {
            base.onDisable();
            Game.stepHeight = .5f;
        }
    }
}
