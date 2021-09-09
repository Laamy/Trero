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
        public AirJump() : base("AirJump", 0x07) { } // 0x07 = no keybind

        public override void onTick()
        {
            Game.onGround = true;
        }

        public override void onEnable()
        {
            Console.WriteLine("Enabled");
            base.onEnable();
        }

        public override void onDisable()
        {
            Console.WriteLine("Disable");
            base.onDisable();
        }
    }
}
