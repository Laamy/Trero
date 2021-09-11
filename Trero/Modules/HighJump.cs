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
    class HighJump : Module
    {
        public HighJump() : base("HighJump", (char)0x07, "HighJump") { } // 0x07 = no keybind

        public override void onTick()
        {
            if (Game.isNull) return;

            if (Keymap.GetAsyncKeyState(Keys.Space));
                Game.velocity = Base.Vec3(0, 5);
        }
    }
}
