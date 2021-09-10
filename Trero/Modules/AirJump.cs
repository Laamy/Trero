using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trero.ClientBase;

namespace Trero.Modules
{
    class AirJump : Module
    {
        public AirJump() : base("AirJump", (char)0x07, "World") { } // 0x07 = no keybind

        public override void onTick()
        {
            if (Game.isNull) return;
            Game.onGround = true;
        }
    }
}
