using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trero.ClientBase;

namespace Trero.Modules
{
    class Antibot : Module
    {
        public Antibot() : base("Antibot", (char)0x07, "Other", true) { } 

        public override void onEnable()
        {
            Game.CustomDefines.antibot = true;
            base.onEnable();
        }

        public override void onDisable()
        {
            Game.CustomDefines.antibot = false;
            base.onDisable();
        }
    }
}
